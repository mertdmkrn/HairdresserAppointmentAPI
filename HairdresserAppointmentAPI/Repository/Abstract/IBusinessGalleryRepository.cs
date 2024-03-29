﻿using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Repository.Abstract
{
    public interface IBusinessGalleryRepository
    {
        Task<BusinessGallery> GetBusinessGalleryByIdAsync(int id);
        Task<IList<BusinessGallery>> GetBusinessGalleryByBusinessIdAsync(int businessId);
        Task<IList<BusinessGallery>> GetBusinessGalleryByBusinessIdAndSizeAsync(int businessId, string size);
        Task<BusinessGallery> SaveBusinessGalleryAsync(BusinessGallery businessGallery);
        Task<IList<BusinessGallery>> SaveBusinessGalleriesAsync(List<BusinessGallery> businessGalleries, int businessId);
        Task<BusinessGallery> UpdateBusinessGalleryAsync(BusinessGallery businessGallery);
        Task<bool> DeleteBusinessGalleryAsync(BusinessGallery businessGallery);
        Task<bool> DeleteBusinessGalleriesAsync(IList<BusinessGallery> businessGalleries);
    }
}
