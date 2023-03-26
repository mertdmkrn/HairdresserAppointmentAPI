using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class ServicesRepository : IServicesRepository
    {
        public async Task<Services> GetServicesByIdAsync(int id)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Services.FindAsync(id);
            }
        }

        public async Task<IList<Services>> GetServicesByBusinessIdAsync(int businessId)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Services
                                .Where(x=>x.businessId == businessId)
                                .OrderBy(x=>x.name)
                                .ToListAsync();
            }
        }

        public async Task<Services> SaveServicesAsync(Services services)
        {
            using (var context = new AppointmentDBContext())
            {
                await context.Services.AddAsync(services);
                await context.SaveChangesAsync();
                return services;
            }
        }

        public async Task<IList<Services>> SaveServicesListAsync(List<Services> servicesList, int businessId)
        {
            using (var context = new AppointmentDBContext())
            {
                servicesList.ForEach(x => { x.businessId = businessId; });
                await context.Services.AddRangeAsync(servicesList);
                await context.SaveChangesAsync();
                return servicesList;
            }
        }

        public async Task<Services> UpdateServicesAsync(Services services)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Services.Update(services);
                await context.SaveChangesAsync();
                return services;
            }
        }

        public async Task<bool> DeleteServicesAsync(Services services)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Services.Remove(services);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteServicesListAsync(IList<Services> servicesList)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Services.RemoveRange(servicesList);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
