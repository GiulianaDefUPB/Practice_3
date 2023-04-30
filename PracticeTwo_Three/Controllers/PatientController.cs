using Microsoft.AspNetCore.Mvc;
using UPB.CoreLogic.Models;
using UPB.CoreLogic.Managers;

namespace UPB.PracticeTwo_Three.Controllers;

[ApiController] //Attributes
[Route("patients")] //URI
// Change route name [Route("[controller]")] 
public class PatientController : ControllerBase //controller base para que sea de tipo web api
{
   private readonly PatientManger _patientManager;
    public PatientController(PatientManger patientManager)
    {
        _patientManager = patientManager;
    }

    [HttpGet]
    public List<Patient> Get()
    {
        return _patientManager.GetAll();
    }

    [HttpGet]
    [Route("{ci}")]
    public Patient GetByID([FromRoute] int ci)
    {
        return _patientManager.GetByID(ci);
    }

    [HttpPut]
    [Route("{ci}")]
    //se puede enivar desde el body un paciente que match ci
    public Patient Put([FromRoute] int ci, [FromBody] Patient patient2Update)
    {
        return _patientManager.Update(ci);
    }

    [HttpPost]
    public Patient Post([FromBody] Patient patient2Create)
    {
        return _patientManager.Create(patient2Create.Name, patient2Create.LastName, patient2Create.CI, patient2Create.BloodType);
    }

    [HttpDelete]
    [Route("{ci}")]
    public Patient Delete([FromRoute] int ci)
    {
        return _patientManager.Delete(ci);
    }
}
