using HairdresserAppointmentAPI.Helpers;
using NetTopologySuite.Geometries;

namespace HairdresserAppointmentAPI.Model.CustomModel
{
    public class BusinessListModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string imagePath { get; set; }
        public double distance { get; set; }
        public double averageRating { get; set; }
        public int countRating { get; set; }

        //public BusinessListModel(Business business, double? latitude, double? longitude)
        //{
        //    double distance = 0;

        //    if (latitude.HasValue && longitude.HasValue)
        //    {
        //        var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        //        var userLocation = gf.CreatePoint(new Coordinate(latitude.Value, longitude.Value));
        //        distance = business.location.Distance(gf.CreateGeometry(userLocation));
        //    }

        //    var galleryItem = business.galleries.FirstOrDefault(x => x.size == Constants.ImageSizes.BusinessListImageSize);

        //    this.id = business.id;
        //    this.name = business.name;
        //    this.city = business.city;
        //    this.province = business.province;
        //    this.imagePath = galleryItem != null ? galleryItem.imagePath : string.Empty;
        //    this.distance = distance;
        //    this.averageRating = business.ratings.Average(x => x.point);
        //    this.countRating = business.ratings.Count();
        //}
    }
}
