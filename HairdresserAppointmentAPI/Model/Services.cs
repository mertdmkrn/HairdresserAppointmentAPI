using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("Service")]
    public class Services
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int businessId { get; set; }
        public Business business { get; set; }
    }
}
