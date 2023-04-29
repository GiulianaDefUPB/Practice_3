using UPB.PracticeTwo_Three.Models;

namespace UPB.PracticeTwo_Three.Managers;

public class PatientManger
{
    private List<Patient> _patients;
    public PatientManger()
    {
        _patients = new List<Patient>();
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
        patientFound = _patients.Find(patient => patient.CI == ci);

        if (patientFound == null)
        {
            throw new Exception("Patient didn't found");
        }
        patientFound.Name = "Cambiado";
        return patientFound;
    }

    public Patient Create(string name, string lastName, int ci, string bloodType)
    {
        Patient createdPatient = new Patient()
        {
            Name = name, 
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