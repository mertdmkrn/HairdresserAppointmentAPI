using HairdresserAppointmentAPI.Helpers;
using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Model.CustomModel;
using HairdresserAppointmentAPI.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Linq;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class BusinessRepository : IBusinessRepository
    {
        public async Task<Business> GetBusinessByEmailAndPasswordAsync(string email, string password)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Businesses
                    .FirstOrDefaultAsync(x => x.email.Equals(email) && x.password.Equals(password.HashString()));
            }
        }

        public async Task<IList<BusinessListModel>> GetBusinessByCityAsync(string city, double? latitude, double? longitude, int? page, int? take)
        {
            using (var context = new AppointmentDBContext())
            {
                Point? userLocation = null;
                var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);

                if (latitude.HasValue && longitude.HasValue)
                {
                    userLocation = gf.CreatePoint(new Coordinate(latitude.Value, longitude.Value));
                }

                if (page.HasValue && take.HasValue)
                {
                    return await context.Businesses
                        .Include(x => x.ratings)
                        .Include(x => x.galleries)
                        .Where(x => x.city.Equals(city))
                        .Select(x => new BusinessListModel
                        {
                            id = x.id,
                            name = x.name,
                            city = x.city,
                            province = x.province,
                            imagePath = x.galleries.FirstOrDefault(x => x.size == Constants.ImageSizes.BusinessListImageSize).imagePath,
                            averageRating = x.ratings.Average(x => x.point),
                            countRating = x.ratings.Count(),
                            distance = userLocation != null ? x.location.Distance(gf.CreateGeometry(userLocation)) : 0
                        })
                        .OrderBy(x => x.distance)
                        .ThenBy(x => x.averageRating)
                        .Skip(page.Value * take.Value)
                        .Take(take.Value)
                        .ToListAsync();
                }

                return await context.Businesses
                    .Include(x => x.ratings)
                    .Include(x => x.galleries)
                    .Where(x => x.city.Equals(city))
                    .Select(x => new BusinessListModel
                    {
                        id = x.id,
                        name = x.name,
                        city = x.city,
                        province = x.province,
                        imagePath = x.galleries.FirstOrDefault(x => x.size == Constants.ImageSizes.BusinessListImageSize).imagePath,
                        averageRating = x.ratings.Average(x => x.point),
                        countRating = x.ratings.Count(),
                        distance = userLocation != null ? x.location.Distance(gf.CreateGeometry(userLocation)) : 0
                    })
                    .OrderBy(x => x.distance)
                    .ThenBy(x => x.averageRating)
                    .ToListAsync();
            }
        }

        public async Task<IList<BusinessListModel>> GetBusinessByCityAndProvinceAsync(string city, string province, double? latitude, double? longitude, int? page, int? take)
        {
            using (var context = new AppointmentDBContext())
            {
                Point? userLocation = null;
                var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);

                if (latitude.HasValue && longitude.HasValue)
                {
                    userLocation = gf.CreatePoint(new Coordinate(latitude.Value, longitude.Value));
                }

                if (page.HasValue && take.HasValue)
                {

                    return await context.Businesses
                        .Include(x => x.ratings)
                        .Include(x => x.galleries)
                        .Where(x => x.city.Equals(city) && x.province.Equals(province))
                        .Select(x => new BusinessListModel
                        {
                            id = x.id,
                            name = x.name,
                            city = x.city,
                            province = x.province,
                            imagePath = x.galleries.FirstOrDefault(x => x.size == Constants.ImageSizes.BusinessListImageSize).imagePath,
                            averageRating = x.ratings.Average(x => x.point),
                            countRating = x.ratings.Count(),
                            distance = userLocation != null ? x.location.Distance(gf.CreateGeometry(userLocation)) : 0
                        })
                        .OrderBy(x => x.distance)
                        .ThenBy(x => x.averageRating)
                        .Skip(page.Value * take.Value)
                        .Take(take.Value)
                        .ToListAsync();
                }

                return await context.Businesses
                    .Include(x => x.ratings)
                    .Include(x => x.galleries)
                    .Where(x => x.city.Equals(city) && x.province.Equals(province))
                    .Select(x => new BusinessListModel
                    {
                        id = x.id,
                        name = x.name,
                        city = x.city,
                        province = x.province,
                        imagePath = x.galleries.FirstOrDefault(x => x.size == Constants.ImageSizes.BusinessListImageSize).imagePath,
                        averageRating = x.ratings.Average(x => x.point),
                        countRating = x.ratings.Count(),
                        distance = userLocation != null ? x.location.Distance(gf.CreateGeometry(userLocation)) : 0
                    })
                    .OrderBy(x => x.distance)
                    .ThenBy(x => x.averageRating)
                    .ToListAsync();
            }
        }

        public async Task<Business> GetBusinessByIdAsync(int id)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Businesses.FindAsync(id);
            }
        }

        public async Task<IList<BusinessListModel>> GetBusinessNearByDistanceAsync(double latitude, double longitude, int metre)
        {
            using (var context = new AppointmentDBContext())
            {
                var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
                var userLocation = gf.CreatePoint(new NetTopologySuite.Geometries.Coordinate(latitude, longitude));

                return await context.Businesses
                    .Include(x => x.ratings)
                    .Include(x => x.galleries)
                    .Where(x => x.location.IsWithinDistance(gf.CreateGeometry(userLocation), metre))
                    .Select(x => new BusinessListModel
                    {
                        id = x.id,
                        name = x.name,
                        city = x.city,
                        province = x.province,
                        imagePath = x.galleries.FirstOrDefault(x => x.size == Constants.ImageSizes.BusinessListImageSize).imagePath,
                        averageRating = x.ratings.Average(x => x.point),
                        countRating = x.ratings.Count(),
                        distance = userLocation != null ? x.location.Distance(gf.CreateGeometry(userLocation)) : 0
                    })
                    .OrderBy(x => x.distance)
                    .ToListAsync();
            }
        }

        public async Task<Business> SaveBusinessAsync(Business business)
        {
            using (var context = new AppointmentDBContext())
            {
                business.password = business.password.HashString();
                business.createDate = DateTime.Now;
                business.updateDate = business.createDate;

                var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
                business.location = gf.CreatePoint(new NetTopologySuite.Geometries.Coordinate(business.latitude, business.longitude));

                await context.Businesses.AddAsync(business);
                await context.SaveChangesAsync();
                return business;
            }
        }

        public async Task<Business> UpdateBusinessAsync(Business business)
        {
            using (var context = new AppointmentDBContext())
            {
                business.updateDate = DateTime.Now;

                context.Businesses.Update(business);
                await context.SaveChangesAsync();
                return business;
            }
        }

        public async Task<bool> DeleteBusinessAsync(Business business)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Businesses.Remove(business);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
