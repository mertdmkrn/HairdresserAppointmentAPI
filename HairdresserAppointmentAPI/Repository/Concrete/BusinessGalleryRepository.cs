using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class BusinessGalleryRepository : IBusinessGalleryRepository
    {
        public async Task<BusinessGallery> GetBusinessGalleryByIdAsync(int id)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Galleries.FindAsync(id);
            }
        }

        public async Task<IList<BusinessGallery>> GetBusinessGalleryByBusinessIdAsync(int businessId)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Galleries
                                .Where(x => x.businessId == businessId)
                                .OrderBy(x => x.size)
                                .ToListAsync();
            }
        }

        public async Task<IList<BusinessGallery>> GetBusinessGalleryByBusinessIdAndSizeAsync(int businessId, string size)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Galleries
                                .Where(x => x.businessId == businessId && x.size == size)
                                .OrderBy(x => x.size)
                                .ToListAsync();
            }
        }

        public async Task<BusinessGallery> SaveBusinessGalleryAsync(BusinessGallery businessGallery)
        {
            using (var context = new AppointmentDBContext())
            {
                await context.Galleries.AddAsync(businessGallery);
                await context.SaveChangesAsync();
                return businessGallery;
            }
        }

        public async Task<IList<BusinessGallery>> SaveBusinessGalleriesAsync(List<BusinessGallery> businessGalleries, int businessId)
        {
            using (var context = new AppointmentDBContext())
            {
                businessGalleries.ForEach(x => { x.businessId = businessId; });
                await context.Galleries.AddRangeAsync(businessGalleries);
                await context.SaveChangesAsync();
                return businessGalleries;
            }
        }

        public async Task<BusinessGallery> UpdateBusinessGalleryAsync(BusinessGallery businessGallery)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Galleries.Update(businessGallery);
                await context.SaveChangesAsync();
                return businessGallery;
            }
        }

        public async Task<bool> DeleteBusinessGalleryAsync(BusinessGallery businessGallery)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Galleries.Remove(businessGallery);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteBusinessGalleriesAsync(IList<BusinessGallery> businessGalleries)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Galleries.RemoveRange(businessGalleries);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
