using Microsoft.OpenApi.Models;
using Serilog;
using UPB.CoreLogic.Managers;
using UPB.PracticeTwo_Three.Middlewares;

//1 create the logger and setup your sinks, filters and properties
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs.log")
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

Log.Information("You are running the app in the {EnvironmentValue} environment", builder.Environment.EnvironmentName);
// 2 Add services to the container.
// Singleton vs Transient vs Scoped
builder.Services.AddTransient<PatientManger>();//almacena los datos globalmente mientras la app siga viva 

builder.Services.AddControllers();
//  Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

IConfiguration Configuration = configurationBuilder.Build();
string siteTitle = Configuration.GetSection("Title").Value;

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = siteTitle
    });
});

// 3 Build
var app = builder.Build();

// 4 Configure the HTTP request pipeline.
app.UseGlobalExceptionHandler();
//app.UseExceptionHandler();
//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseCors();
//app.UseAuthentication();
//app.UseAuthorization();
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "QA" || app.Environment.EnvironmentName == "UAT")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// 5 Run
app.Run();
