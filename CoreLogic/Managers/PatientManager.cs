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
         if (ci < 0)
        {
            throw new Exception("Invalid CI");
        } 
        Patient patientFound = _patients.Find(patient => patient.CI == ci); // predicados

        if (patientFound == null)
        {
            throw new Exception("Patient not found");
        }
        return patientFound;
        // Find propio de Enumerable
    }

    public Patient Update(int ci, string name, string lastName)
    {
       
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        } 
        
        Patient patientFound;
        patientFound = _patients.Find(patient => patient.CI == ci); // predicados

        if (patientFound == null)
        {
            throw new Exception("Patient not found");
        }
        patientFound.Name = name;
        patientFound.LastName = lastName;
        return patientFound;
    }

    public Patient Create(string name, string lastName, int ci)
    {
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        } 

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
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        } 

        int patient2DeleteIndex = _patients.FindIndex(patient => patient.CI == ci);
        if (patient2DeleteIndex == -1)
        {
            throw new Exception("Patient not found");
        }
        Patient patient2Delete = _patients[patient2DeleteIndex];
        _patients.RemoveAt(patient2DeleteIndex);
        return patient2Delete;
    }
}