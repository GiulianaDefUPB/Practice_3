using System.Reflection;
using UPB.CoreLogic.Models;

namespace UPB.CoreLogic.Managers;

public class PatientManger
{
    private List<Patient> _patients;
    private List<string> _bloodTypes;
    public PatientManger()
    {
        _patients = new List<Patient>();
        _bloodTypes = new List<string> { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-"};
    }
    public List<Patient> GetAll(string path)
    {
        /*
        StreamReader reader = new StreamReader(path);

        string line = reader.ReadLine();

        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
        // do something with the line…
        }
        reader.Close();*/
        if (_patients.Count > 0)
            _patients[0].Name = path;
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

    public Patient Create(string name, string lastName, int ci, string path)
    {
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        } 

        var rand = new Random();
        string bloodType = _bloodTypes[rand.Next(0,_bloodTypes.Count)];

        Patient createdPatient = new Patient()
        {
            Name = name, // capitalize
            LastName = lastName,
            CI = ci,
            BloodType = bloodType
        };

        _patients.Add(createdPatient);

        List<String> patientData = new List<string>();
        
        foreach (PropertyInfo property in createdPatient.GetType().GetProperties())
        {
            patientData.Add(property.GetValue(createdPatient).ToString());
        }

        StreamWriter writer = new StreamWriter(path, true);
        string rawData = string.Join(",", patientData);
        writer.WriteLine(rawData);
        writer.Close();

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