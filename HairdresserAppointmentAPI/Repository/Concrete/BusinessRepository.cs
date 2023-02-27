using HairdresserAppointmentAPI.Helpers;
using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class BusinessRepository : IBusinessRepository
    {
        public async Task<Business> GetBusinessByEmailAndPasswordAsync(string email, string password)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Businesses.FirstOrDefaultAsync(x => x.email.Equals(email) && x.password.Equals(password.HashString()));
            }
        }

        public async Task<IList<Business>> GetBusinessByCountryAsync(string country, int page = 0, int take = 10)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Businesses
                    .Where(x => x.country.Equals(country))
                    .OrderBy(x=>x.name)
                    .Skip(page * take)
                    .Take(take)
                    .ToListAsync();
            }
        }

        public async Task<IList<Business>> GetBusinessByCountryAndProvinceAsync(string country, string province, int page = 0, int take = 10)
        {
            using (var context = new AppointmentDBContext())
            {
                return context.Businesses
                    .Where(x => x.country.Equals(country) && x.province.Equals(province))
                    .OrderBy(x => x.name)
                    .Skip(page * take)
                    .Take(take)
                    .ToList();
            }
        }

        public async Task<Business> GetBusinessByIdAsync(int id)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Businesses.FindAsync(id);
            }
        }

        public async Task<IList<Business>> GetBusinessNearByDistanceAsync(double latitude, double longitude, int metre)
        {
            using (var context = new AppointmentDBContext())
            {
                var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
                var userLocation = gf.CreatePoint(new NetTopologySuite.Geometries.Coordinate(latitude, longitude));

                return await context.Businesses
                    .Where(x => x.location.IsWithinDistance(gf.CreateGeometry(userLocation), metre))
                    .OrderBy (x => x.location.Distance(gf.CreateGeometry(userLocation)))
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
