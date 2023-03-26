using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("BusinessCategory")]
    public class BusinessCategory
    {
        [Key]
        public int id { get; set; }
        public int businessId { get; set; }
        public int categoryId { get; set; }
        public Business? business { get; set; }
        public Category? category { get; set; }

    }
}
