using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Repository.Concrete;
using HairdresserAppointmentAPI.Service.Abstract;

namespace HairdresserAppointmentAPI.Service.Concrete
{
    public class BusinessCategoryService : IBusinessCategoryService
    {
        private IBusinessCategoryRepository _businessCategoryRepository;

        public BusinessCategoryService()
        {
            _businessCategoryRepository = new BusinessCategoryRepository();
        }

        public async Task<BusinessCategory> GetBusinessCategoryByIdAsync(int id)
        {
            return await _businessCategoryRepository.GetBusinessCategoryByIdAsync(id);
        }

        public async Task<IList<BusinessCategory>> GetBusinessCategoryByBusinessIdAsync(int businessId)
        {
            return await _businessCategoryRepository.GetBusinessCategoryByBusinessIdAsync(businessId);
        }

        public async Task<IList<BusinessCategory>> GetBusinessCategoryByCategoryIdAsync(int categoryId)
        {
            return await _businessCategoryRepository.GetBusinessCategoryByCategoryIdAsync(categoryId);
        }

        public async Task<BusinessCategory> SaveBusinessCategoryAsync(BusinessCategory businessCategory)
        {
            return await _businessCategoryRepository.SaveBusinessCategoryAsync(businessCategory);
        }

        public async Task<BusinessCategory> UpdateBusinessCategoryAsync(BusinessCategory businessCategory)
        {
            return await _businessCategoryRepository.UpdateBusinessCategoryAsync(businessCategory);
        }

        public async Task<bool> DeleteBusinessCategoryAsync(BusinessCategory businessCategory)
        {
            return await _businessCategoryRepository.DeleteBusinessCategoryAsync(businessCategory);
        }

        public async Task<bool> DeleteBusinessCategoriesAsync(IList<BusinessCategory> businessCategories)
        {
            return await _businessCategoryRepository.DeleteBusinessCategoriesAsync(businessCategories);
        }
    }
}
