using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Service.Abstract
{
    public interface IRatingService
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
