﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairdresserAppointmentAPI.Model
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
