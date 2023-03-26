using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class BusinessCategoryRepository : IBusinessCategoryRepository
    {
        public async Task<BusinessCategory> GetBusinessCategoryByIdAsync(int id)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.BusinessCategories.FindAsync(id);
            }
        }

        public async Task<IList<BusinessCategory>> GetBusinessCategoryByBusinessIdAsync(int businessId)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.BusinessCategories
                                .Where(x => x.businessId == businessId)
                                .ToListAsync();
            }
        }

        public async Task<IList<BusinessCategory>> GetBusinessCategoryByCategoryIdAsync(int categoryId)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.BusinessCategories
                                .Where(x => x.categoryId == categoryId)
                                .ToListAsync();
            }
        }

        public async Task<BusinessCategory> SaveBusinessCategoryAsync(BusinessCategory businessCategory)
        {
            using (var context = new AppointmentDBContext())
            {
                await context.BusinessCategories.AddAsync(businessCategory);
                await context.SaveChangesAsync();
                return businessCategory;
            }
        }

        public async Task<BusinessCategory> UpdateBusinessCategoryAsync(BusinessCategory businessCategory)
        {
            using (var context = new AppointmentDBContext())
            {
                context.BusinessCategories.Update(businessCategory);
                await context.SaveChangesAsync();
                return businessCategory;
            }
        }

        public async Task<bool> DeleteBusinessCategoryAsync(BusinessCategory businessCategory)
        {
            using (var context = new AppointmentDBContext())
            {
                context.BusinessCategories.Remove(businessCategory);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteBusinessCategoriesAsync(IList<BusinessCategory> businessCategories)
        {
            using (var context = new AppointmentDBContext())
            {
                context.BusinessCategories.RemoveRange(businessCategories);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
