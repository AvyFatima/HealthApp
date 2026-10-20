
using System.Globalization;
using System.Xml.Serialization;

namespace HealthApp.Model
{
	public class Patient
	{
        [XmlAttribute("Id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string BloodGroup { get; set; }

        public string Address { get; set; }

        [XmlElement("AddedDate")]
        public string RawAddedDate { get; set; }

        [XmlIgnore]
        public DateTime AddedDate
        {
            get
            {
                // Converts "15/09/2026 14:00:00" dynamically into a real DateTime object
                if (DateTime.TryParseExact(RawAddedDate, AppConstants.AllowedDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime parsedDate))
                {
                    return parsedDate;
                }
                return DateTime.MinValue; // Fallback if format is corrupted
            }
            set
            {
                // Ensures when you save back to XML, it writes it in your preferred style
                RawAddedDate = value.ToString(AppConstants.StandardDateFormat);
            }
        }

    }
}

