using System;
using System.Globalization;
using System.Xml.Serialization;

namespace HealthApp.Model
{

    public class Appointment
	{

        [XmlAttribute("Id")]
        public int AptnId { get; set; }

        // 1. Tell XML Serializer to capture the raw tag text string from  file
        [XmlElement("AptnDate")]
        public string RawAptnDate { get; set; }

        // 2. Tell the serializer to COMPLETELY IGNORE this real DateTime property
        [XmlIgnore]
        public DateTime AptnDate
        {
            get
            {
                // Converts "15/09/2026 14:00:00" dynamically into a real DateTime object
                if (DateTime.TryParseExact(RawAptnDate, AppConstants.AllowedDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime parsedDate))
                {
                    return parsedDate;
                }
                return DateTime.MinValue; // Fallback if format is corrupted
            }
            set
            {
                // Ensures when you save back to XML, it writes it in your preferred style
                RawAptnDate = value.ToString(AppConstants.StandardDateFormat);
            }
        }

        // 3. This is a simple string property that will be serialized as-is
        public String AptnDateLabel { get; set; }

        public String AptnDetails { get; set; }

        public int AptnPatientId { get; set; }

        [XmlIgnore]
        public String AptnPatientName { get; set; }

        public int AssignedToId { get; set; }

        public bool isDeleted { get; set; }

        [XmlElement("UpdatedOn")]
        public string RawUpdatedOn { get; set; }

        [XmlIgnore]
        public DateTime UpdatedOn
        {
            get
            {
                // Converts "15/09/2026 14:00:00" dynamically into a real DateTime object
                if (DateTime.TryParseExact(RawUpdatedOn, AppConstants.AllowedDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime parsedDate))
                {
                    return parsedDate;
                }
                return DateTime.MinValue; // Fallback if format is corrupted
            }
            set
            {
                // Ensures when you save back to XML, it writes it in your preferred style
                RawUpdatedOn = value.ToString(AppConstants.StandardDateFormat);
            }
        }

        public String OtherDetails { get; set; }

        [XmlArray("Notes")]
        [XmlArrayItem("Note")]
        public String[] Notes { get; set; }

        [XmlArray("Reminders")]
        [XmlArrayItem("Reminder")]
        public String[] Reminders { get; set; }

    }
}

