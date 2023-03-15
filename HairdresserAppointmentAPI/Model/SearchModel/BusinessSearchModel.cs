namespace HairdresserAppointmentAPI.Model.SearchModel
{
    public class BusinessSearchModel
    {
        public string? City { get; set; }
        public string? Province { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? Page { get; set; }
        public int? Take { get; set; }
    }
}
