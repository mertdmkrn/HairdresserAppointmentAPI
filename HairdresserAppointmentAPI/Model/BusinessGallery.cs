using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("BusinessGallery")]
    public class BusinessGallery
    {
        [Key]
        public int id { get; set; }
        public string imagePath { get; set; }
        public string size { get; set; }
        public int businessId { get; set; }
        public Business business { get; set; }
    }
}
