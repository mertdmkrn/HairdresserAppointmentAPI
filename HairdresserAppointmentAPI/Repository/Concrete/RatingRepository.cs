using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class RatingRepository : IRatingRepository
    {
        public async Task<Rating> GetRatingByIdAsync(int id)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Ratings.FindAsync(id);
            }
        }

        public async Task<IList<Rating>> GetRatingsByBusinessIdAsync(int businessId)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Ratings.Where(x => x.businessId == businessId).ToListAsync();
            }
        }     
        
        public async Task<IList<Rating>> GetRatingsByUserIdAsync(int userId)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Ratings.Where(x => x.userId == userId).ToListAsync();
            }
        }
             
        public async Task<IList<Rating>> GetRatingsByUserIdWithBusinessIdAsync(int userId, int businessId)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Ratings.Where(x => x.userId == userId && x.businessId == businessId).ToListAsync();
            }
        }

        public async Task<Rating> SaveRatingAsync(Rating rating)
        {
            using (var context = new AppointmentDBContext())
            {
                rating.createDate = DateTime.Now;
                rating.updateDate = rating.createDate;

                await context.Ratings.AddAsync(rating);
                await context.SaveChangesAsync();
                return rating;
            }
        }

        public async Task<Rating> UpdateRatingAsync(Rating rating)
        {
            using (var context = new AppointmentDBContext())
            {
                rating.updateDate = DateTime.Now;

                context.Ratings.Update(rating);
                await context.SaveChangesAsync();
                return rating;
            }
        }

        public async Task<bool> DeleteRatingAsync(Rating rating)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Ratings.Remove(rating);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
