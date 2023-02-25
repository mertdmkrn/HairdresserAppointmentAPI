using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("BusinessCategory")]
    public class BusinessGallery
    {
        [Key]
        public int id { get; set; }
        public string imagePath { get; set; }
        public int businessId { get; set; }
    }
}
