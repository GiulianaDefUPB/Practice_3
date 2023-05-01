using UPB.CoreLogic.Models;

namespace UPB.CoreLogic.Managers;

public class PatientManger
{
    private List<Patient> _patients;
    private List<string> _bloodTypes;
    public PatientManger()
    {
        _patients = new List<Patient>();
        _bloodTypes = new List<string> { "A", "B", "O", "AB"};
    }
    public List<Patient> GetAll()
    {
        
        return _patients;
    }

    public Patient GetByID(int ci)
    {
        return _patients.Find(patient => patient.CI == ci);
        // Find propio de Enumerable
    }

    public Patient Update(int ci)
    {
       
        if (ci < 0)
        {
            throw new Exception("CI invalido");
        } 
        
        Patient patientFound;
        patientFound = _patients.Find(patient => patient.CI == ci); // predicados

        if (patientFound == null)
        {
            throw new Exception("Patient didn't found");
        }
        patientFound.Name = "Cambiado";
        return patientFound;
    }

    public Patient Create(string name, string lastName, int ci)
    {
        var rand = new Random();
        string bloodType = _bloodTypes[rand.Next(0,5)];
        Patient createdPatient = new Patient()
        {
            Name = name, // capitalize
            LastName = lastName,
            CI = ci,
            BloodType = bloodType
        };

        _patients.Add(createdPatient);
        return createdPatient;
    }
    public Patient Delete(int ci)
    {
        int patient2DeleteIndex = _patients.FindIndex(patient => patient.CI == ci);
        _patients.RemoveAt(patient2DeleteIndex);
        return new Patient();
    }
}