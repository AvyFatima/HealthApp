using System.Globalization;
using System.Xml.Serialization;

namespace HealthApp.Model
{
	public sealed class Employee
    {
        [XmlAttribute("Id")] 
        public int Id { get; init; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Username { get; init; } = string.Empty;

        [XmlElement("password")]
        public string PasswordHash { get; init; } = string.Empty;

        // public string password { get; set; }


        // 1. Tell XML Serializer to capture the raw tag text string from  file
        [XmlElement("JoiningDate")]
        public string RawJoiningDate { get; set; }

        // 2. Tell the serializer to COMPLETELY IGNORE this real DateTime property
        [XmlIgnore]
        public DateTime JoiningDate
        {
            get
            {
                // Converts "15/09/2026 14:00:00" dynamically into a real DateTime object
                if (DateTime.TryParseExact(RawJoiningDate, AppConstants.AllowedDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime parsedDate))
                {
                    return parsedDate;
                }
                return DateTime.MinValue; // Fallback if format is corrupted
            }
            set
            {
                // Ensures when you save back to XML, it writes it in your preferred style
                RawJoiningDate = value.ToString(AppConstants.StandardDateFormat);
            }
        }

    }
}

