using System.Xml.Serialization;
using HealthApp.Model;

namespace HealthApp.DataMap
{
    // Defines the root element of the XML file
    [XmlRoot("EmployeeDatabase")]
    public class EmployeeDatabase
    {
        [XmlArray("Employees")]
        [XmlArrayItem("Employee")]
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }

    
    [XmlRoot("AppointmentDatabase")]
    public class AppointmentDatabase
    {
        [XmlArray("Appointments")]
        [XmlArrayItem("Appointment")]
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

    
    [XmlRoot("PatientDatabase")]
    public class PatientDatabase
    {
        [XmlArray("Patients")]
        [XmlArrayItem("Patient")]
        public List<Patient> Patients { get; set; } = new List<Patient>();
    }
}

