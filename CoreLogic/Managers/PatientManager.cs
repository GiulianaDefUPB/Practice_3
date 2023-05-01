using System.Reflection;
using UPB.CoreLogic.Models;

namespace UPB.CoreLogic.Managers;

public class PatientManger
{
    private List<string> _bloodTypes;
    public PatientManger()
    {
        _bloodTypes = new List<string> { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-"};
    }
    public List<Patient> GetAll(string path)
    {
        if (!File.Exists(path))
        {
            throw new Exception("Empty data base.");
        }
        List<Patient> patientList = new List<Patient>();

        int numProperties = typeof(Patient).GetProperties().Length;
        string line;
        string[] dataArray = new string[numProperties];

        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            dataArray = line.Split(",");

            patientList.Add(new Patient()
            {
                Name = dataArray[0],
                LastName = dataArray[1],
                CI = Int32.Parse(dataArray[2]),
                BloodType = dataArray[3]
            });             
        }
        reader.Close();
        /*
        if (_patients.Count > 0)
            _patients[0].Name = path;*/
        return patientList;
    }

    public Patient GetByID(int ci, string path)
    {
        if (!File.Exists(path))
        {
            throw new Exception("Empty database.");
        }

         if (ci < 0)
        {
            throw new Exception("Invalid CI");
        } 
        Patient patientFound = this.Search(ci, path); // predicados

        if (patientFound == null)
        {
            throw new Exception("Patient not found");
        }

        return patientFound;
        // Find propio de Enumerable
    }

    public Patient Update(int ci, string name, string lastName, string path)
    {
        if (!File.Exists(path))
        {
            throw new Exception("Empty data base.");
        }
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        } 
        
        Patient patientFound;
        patientFound = this.Search(ci, path); // predicados

        if (patientFound == null)
        {
            throw new Exception("Patient not found");
        }

        List<string> lst = File.ReadAllLines(path).Where(arg => !string.IsNullOrWhiteSpace(arg)).ToList();  
        int index = lst.FindIndex(x => x.Split(',')[2].Equals(ci.ToString())); 
        
        List<String> patientData = new List<string>();
        
        foreach (PropertyInfo property in patientFound.GetType().GetProperties())
        {
            patientData.Add(property.GetValue(patientFound).ToString());
        }

        patientData[0] = name;
        patientData[1] = lastName;
        string rawData = string.Join(",", patientData);
        lst[index] = rawData;
        File.WriteAllLines(path, lst);  
        
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
    
    public Patient Delete(int ci, string path)
    {
        if (!File.Exists(path))
        {
            throw new Exception("Empty data base.");
        }
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        } 

        Patient patient2Delete = this.Search(ci, path);
        if (patient2Delete == null)
        {
            throw new Exception("Patient not found");
        }

        List<string> lst = File.ReadAllLines(path).Where(arg => !string.IsNullOrWhiteSpace(arg)).ToList();  
        lst.RemoveAll(x => x.Split(',')[2].Equals(ci.ToString()));  
        File.WriteAllLines(path, lst);  
        return patient2Delete;
    }

    private Patient Search(int ci, string path)
    {
        StreamReader reader = new StreamReader(path);

        int numProperties = typeof(Patient).GetProperties().Length;
        string line;
        string[] dataArray = new string[numProperties];
        Patient foundPatient = null;

        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            dataArray = line.Split(",");
            if (Int32.Parse(dataArray[2]) == ci)
            {
                foundPatient = new Patient()
                {
                    Name = dataArray[0], // capitalize
                    LastName = dataArray[1],
                    CI = Int32.Parse(dataArray[2]),
                    BloodType = dataArray[3]
                }; 
                break;
            }   
        }
        
        reader.Close();
        
        return foundPatient;
    }
}