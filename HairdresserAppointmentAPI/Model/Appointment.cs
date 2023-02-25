﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace HairdresserAppointmentAPI.Model
{
    [Table("Appointment")]
    public class Appointment
    {
        [Key]
        public long id { get; set; }
        public DateTime? date { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }
        public string description { get; set; }
        public int status { get; set; }
        public int userId { get; set; }
        public int businessId { get; set; }
    }
}