using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("user")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userFullName { get; set; }
        public string userEmail { get; set; }
        public string userPassword { get; set; }
        public DateTime userCreateDate { get; set; }
        public DateTime userUpdateDate { get; set; }
        public string userImagePath { get; set; }

    }
}
