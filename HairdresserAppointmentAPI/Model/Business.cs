using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("Business")]
    public class Business
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string address { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public bool verified { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }
        public string imagePath { get; set; }
        public int workingType { get; set; }
        public string workingStartHour { get; set; }
        public string workingEndHour { get; set; }
        public int appointmentTimeInterval { get; set; }
        public int appointmentPeopleCount { get; set; }
        public bool officialHolidayAvailable { get; set; }
        public int status { get; set; }
    }
}
