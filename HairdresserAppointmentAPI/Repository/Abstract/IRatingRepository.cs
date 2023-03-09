using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Repository.Abstract
{
    public interface IRatingRepository
    {
        Task<Rating> GetRatingByIdAsync(int id);
        Task<IList<Rating>> GetRatingsByBusinessIdAsync(int businessId);
        Task<IList<Rating>> GetRatingsByUserIdAsync(int userId);
        Task<IList<Rating>> GetRatingsByUserIdWithBusinessIdAsync(int userId, int businessId);
        Task<Rating> SaveRatingAsync(Rating rating);
        Task<Rating> UpdateRatingAsync(Rating rating);
        Task<bool> DeleteRatingAsync(Rating rating);
    }
}
