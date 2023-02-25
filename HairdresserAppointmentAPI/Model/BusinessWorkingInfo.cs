using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("BusinessWorkingInfo")]
    public class BusinessWorkingInfo
    {
        [Key]
        public long id { get; set; }
        public DateTime? date { get; set; }
        public string startHour { get; set; }
        public string endHour { get; set; }
        public int appointmentTimeInterval { get; set; }
        public int appointmentPeopleCount { get; set; }
        public int businessId { get; set; }
    }
}
