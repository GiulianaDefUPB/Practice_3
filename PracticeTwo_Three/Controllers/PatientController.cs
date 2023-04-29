using Microsoft.AspNetCore.Mvc;
using UPB.PracticeTwo_Three.Models;

namespace UPB.PracticeTwo_Three.Controllers;

[ApiController] //Attributes
[Route("patients")] //URI
// Change route name [Route("[controller]")] 
public class PatientController : ControllerBase //controller base para que sea de tipo web api
{
   
    public PatientController(){}

    [HttpGet]
    public List<Patient> Get()
    {
        return new List<Patient>();
    }

    [HttpGet]
    [Route("{ci}")]
    public Patient GetByID([FromRoute] int ci)
    {
        return new Patient();
    }

    [HttpPut]
    [Route("{ci}")]
    public Patient Put([FromRoute] int ci)
    {
        return new Patient();
    }

    [HttpPost]
    public Patient Post()
    {
        return new Patient();
    }

    [HttpDelete]
    public Patient Delete()
    {
        return new Patient();
    }
}
