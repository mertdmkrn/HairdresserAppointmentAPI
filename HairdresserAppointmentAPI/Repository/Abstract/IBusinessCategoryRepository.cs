using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Repository.Abstract
{
    public interface IBusinessCategoryRepository
    {
        Task<BusinessCategory> GetBusinessCategoryByIdAsync(int id);
        Task<IList<BusinessCategory>> GetBusinessCategoryByBusinessIdAsync(int businessId);
        Task<IList<BusinessCategory>> GetBusinessCategoryByCategoryIdAsync(int categoryId);
        Task<BusinessCategory> SaveBusinessCategoryAsync(BusinessCategory businessCategory);
        Task<BusinessCategory> UpdateBusinessCategoryAsync(BusinessCategory businessCategory);
        Task<bool> DeleteBusinessCategoryAsync(BusinessCategory businessCategory);
    }
}
