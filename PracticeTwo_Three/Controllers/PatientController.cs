using Microsoft.AspNetCore.Mvc;
using UPB.PracticeTwo_Three.Models;
using UPB.PracticeTwo_Three.Managers;

namespace UPB.PracticeTwo_Three.Controllers;

[ApiController] //Attributes
[Route("patients")] //URI
// Change route name [Route("[controller]")] 
public class PatientController : ControllerBase //controller base para que sea de tipo web api
{
   private PatientManger _patientManager;
    public PatientController()
    {
        _patientManager = new PatientManger();
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
    public Patient Put([FromRoute] int ci)
    {
        return _patientManager.Put(ci);
    }

    [HttpPost]
    public Patient Post()
    {
        return _patientManager.Post();
    }

    [HttpDelete]
    public Patient Delete()
    {
        return new Patient();
    }
}
