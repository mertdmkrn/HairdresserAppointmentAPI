using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("User")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(150)]
        public string? fullName { get; set; }

        [Required]
        [MaxLength(10)]
        public string? gender { get; set; }

        [Required]
        [MaxLength(150)]
        public string? email { get; set; }

        [Required]
        [MaxLength(11)]
        public string? telephone { get; set; }

        [Required]
        [MaxLength(8)]
        public string? password { get; set; }

        public string? imagePath { get; set; }


        [Required]
        public DateTime? createDate { get; set; }

        [Required]
        public DateTime? updateDate { get; set; }

        [Required]
        public DateTime? birthDate { get; set; }

        public string? services { get; set; }

        [Required]
        public bool verified { get; set; }

    }
}
