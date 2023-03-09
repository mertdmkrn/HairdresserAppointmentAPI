using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("Rating")]
    public class Rating
    {
        [Key]
        public int id { get; set; }
        public string comment { get; set; }
        public double point { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }
        public int businessId { get; set; }
        public int userId { get; set; }
    }
}
