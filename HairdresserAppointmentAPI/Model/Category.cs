using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            this.categories = new HashSet<BusinessCategory>();
        }

        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public virtual ICollection<BusinessCategory> categories { get; set; }

    }
}
