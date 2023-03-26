using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HairdresserAppointmentAPI.Service.Concrete;
using NetTopologySuite.Geometries;

namespace HairdresserAppointmentAPI.Model
{
    [Table("Business")]
    public class Business
    {

        public Business()
        {
            this.ratings = new HashSet<Rating>();
            this.services = new HashSet<Services>();
            this.categories = new HashSet<BusinessCategory>();
            this.galleries = new HashSet<BusinessGallery>();
            this.workingInfos = new HashSet<BusinessWorkingInfo>();
            this.appointments = new HashSet<Appointment>();
        }


        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? city { get; set; }
        public string? province { get; set; }
        public string? district { get; set; }
        public string? address { get; set; }
        public string? telephone { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        [JsonIgnore]
        public Point? location { get; set; }

        public bool verified { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }
        public int workingType { get; set; }
        public string? workingStartHour { get; set; }
        public string? workingEndHour { get; set; }
        public int appointmentTimeInterval { get; set; }
        public int appointmentPeopleCount { get; set; }
        public bool officialHolidayAvailable { get; set; }
        public virtual ICollection<Rating> ratings { get; set; }
        public virtual ICollection<BusinessGallery> galleries { get; set; }
        public virtual ICollection<Services> services { get; set; }
        public virtual ICollection<BusinessCategory> categories { get; set; }
        public virtual ICollection<BusinessWorkingInfo> workingInfos { get; set; }
        public virtual ICollection<Appointment> appointments { get; set; }
    }
}
