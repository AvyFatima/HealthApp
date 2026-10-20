using System;

namespace HealthApp.Persistence.XmlModel
{
	public class AppointmentXmlModel
	{
		public int Id { get; set; }

		public int PatientId { get; set; }

		public int DoctorId { get; set; }

		public DateTime Date { get; set; }

		public string Notes { get; set; }
	}
}
