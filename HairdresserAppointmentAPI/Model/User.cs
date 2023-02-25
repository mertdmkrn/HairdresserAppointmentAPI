using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("User")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string? fullName { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }
        public string? imagePath { get; set; }
        public bool verified { get; set; }

    }
}
