using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Service.Abstract
{
    public interface IBusinessGalleryService
    {
        Task<BusinessGallery> GetBusinessGalleryByIdAsync(int id);
        Task<IList<BusinessGallery>> GetBusinessGalleryByBusinessIdAsync(int businessId);
        Task<IList<BusinessGallery>> GetBusinessGalleryByBusinessIdAndSizeAsync(int businessId, string size);
        Task<BusinessGallery> SaveBusinessGalleryAsync(BusinessGallery businessGallery);
        Task<IList<BusinessGallery>> SaveBusinessGalleriesAsync(List<BusinessGallery> businessGalleries, int businessId);
        Task<BusinessGallery> UpdateBusinessGalleryAsync(BusinessGallery businessGallery);
        Task<bool> DeleteBusinessCategoryAsync(BusinessGallery businessGallery);
        Task<bool> DeleteBusinessGalleriesAsync(List<BusinessGallery> businessGalleries);
    }
}
