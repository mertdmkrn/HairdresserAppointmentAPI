using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Categories
                    .FindAsync(id);
            }
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Categories
                    .FirstOrDefaultAsync(x=>x.name.Equals(name));
            }
        }

        public async Task<IList<Category>> GetCategoriesAsync()
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Categories
                    .ToListAsync();
            }
        }

        public async Task<Category> SaveCategoryAsync(Category category)
        {
            using (var context = new AppointmentDBContext())
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return category;
            }
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Categories.Update(category);
                await context.SaveChangesAsync();
                return category;
            }
        }

        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
