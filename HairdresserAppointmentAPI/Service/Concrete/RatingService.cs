using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Repository.Concrete;
using HairdresserAppointmentAPI.Service.Abstract;

namespace HairdresserAppointmentAPI.Service.Concrete
{
    public class RatingService : IRatingService
    {
        private IRatingRepository _ratingRepository;
        
        public RatingService() {
            _ratingRepository = new RatingRepository();
        }

        public async Task<Rating> GetRatingByIdAsync(int id)
        {
            return await _ratingRepository.GetRatingByIdAsync(id);
        }

        public async Task<IList<Rating>> GetRatingsByBusinessIdAsync(int businessId)
        {
            return await _ratingRepository.GetRatingsByBusinessIdAsync(businessId);
        }

        public async Task<IList<Rating>> GetRatingsByUserIdAsync(int userId)
        {
            return await _ratingRepository.GetRatingsByUserIdAsync(userId);
        }

        public async Task<IList<Rating>> GetRatingsByUserIdWithBusinessIdAsync(int userId, int businessId)
        {
            return await _ratingRepository.GetRatingsByUserIdWithBusinessIdAsync(userId, businessId);
        }

        public async Task<Rating> SaveRatingAsync(Rating rating)
        {
            return await _ratingRepository.SaveRatingAsync(rating);
        }

        public async Task<Rating> UpdateRatingAsync(Rating rating)
        {
            return await _ratingRepository.UpdateRatingAsync(rating);
        }

        public async Task<bool> DeleteRatingAsync(Rating rating)
        {
            return await _ratingRepository.DeleteRatingAsync(rating);
        }
    }
}
