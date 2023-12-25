using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Repository.Concrete;
using HairdresserAppointmentAPI.Service.Abstract;

namespace HairdresserAppointmentAPI.Service.Concrete
{
    public class BusinessGalleryService : IBusinessGalleryService
    {
        private IBusinessGalleryRepository _businessGalleryRepository;

        public BusinessGalleryService(IBusinessGalleryRepository businessGalleryRepository)
        {
            _businessGalleryRepository = businessGalleryRepository;
        }

        public async Task<bool> DeleteBusinessGalleryAsync(BusinessGallery businessGallery)
        {
            return await _businessGalleryRepository.DeleteBusinessGalleryAsync(businessGallery);
        }

        public async Task<bool> DeleteBusinessGalleriesAsync(IList<BusinessGallery> businessGalleries)
        {
            return await _businessGalleryRepository.DeleteBusinessGalleriesAsync(businessGalleries);
        }

        public async Task<IList<BusinessGallery>> GetBusinessGalleryByBusinessIdAndSizeAsync(int businessId, string size)
        {
            return await _businessGalleryRepository.GetBusinessGalleryByBusinessIdAndSizeAsync(businessId, size);
        }

        public async Task<IList<BusinessGallery>> GetBusinessGalleryByBusinessIdAsync(int businessId)
        {
            return await _businessGalleryRepository.GetBusinessGalleryByBusinessIdAsync(businessId);
        }

        public async Task<BusinessGallery> GetBusinessGalleryByIdAsync(int id)
        {
            return await _businessGalleryRepository.GetBusinessGalleryByIdAsync(id);
        }

        public async Task<BusinessGallery> SaveBusinessGalleryAsync(BusinessGallery businessGallery)
        {
            return await _businessGalleryRepository.SaveBusinessGalleryAsync(businessGallery);
        }

        public async Task<IList<BusinessGallery>> SaveBusinessGalleriesAsync(List<BusinessGallery> businessGalleries, int businessId)
        {
            return await _businessGalleryRepository.SaveBusinessGalleriesAsync(businessGalleries, businessId);
        }

        public async Task<BusinessGallery> UpdateBusinessGalleryAsync(BusinessGallery businessGallery)
        {
            return await _businessGalleryRepository.UpdateBusinessGalleryAsync(businessGallery);
        }
    }
}
