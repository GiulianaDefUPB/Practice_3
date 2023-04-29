using Microsoft.AspNetCore.Mvc;

namespace UPB.PracticeTwo_Three.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase //controller base para que sea de tipo web api
{
   

   
    public PatientController()
    {
        
    }

   
    public IEnumerable<Patient> Get()
    {
        return new List<Patient>();
    }
}
