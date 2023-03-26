using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class BusinessWorkingInfoRepository : IBusinessWorkingInfoRepository
    {
        public async Task<BusinessWorkingInfo> GetBusinessWorkingInfoByIdAsync(long id)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.BusinessWorkingInfos.FindAsync(id);
            }
        }

        public async Task<IList<BusinessWorkingInfo>> GetBusinessWorkingInfoByBusinessIdAsync(int businessId)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.BusinessWorkingInfos
                                .Where(x => x.businessId == businessId)
                                .OrderBy(x => x.date)
                                .ToListAsync();
            }
        }

        public async Task<IList<BusinessWorkingInfo>> GetBusinessWorkingInfoByBusinessIdAndDateAsync(int businessId, DateTime? date)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.BusinessWorkingInfos
                                .Where(x => x.businessId == businessId && x.date.Value.Date == date.Value.Date)
                                .ToListAsync();
            }
        }

        public async Task<IList<BusinessWorkingInfo>> GetBusinessWorkingInfoByBusinessIdAndBetweenDateAsync(int businessId, DateTime? startDate, DateTime? endDate)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.BusinessWorkingInfos
                                .Where(x => x.businessId == businessId && x.date.Value.Date >= startDate.Value.Date && x.date.Value.Date <= endDate.Value.Date)
                                .OrderBy(x => x.date)
                                .ToListAsync();
            }
        }

        public async Task<BusinessWorkingInfo> SaveBusinessWorkingInfoAsync(BusinessWorkingInfo businessWorkingInfo)
        {
            using (var context = new AppointmentDBContext())
            {
                await context.BusinessWorkingInfos.AddAsync(businessWorkingInfo);
                await context.SaveChangesAsync();
                return businessWorkingInfo;
            }
        }

        public async Task<IList<BusinessWorkingInfo>> SaveBusinessWorkingInfosAsync(List<BusinessWorkingInfo> businessWorkingInfos, int businessId)
        {
            using (var context = new AppointmentDBContext())
            {
                businessWorkingInfos.ForEach(x => { x.businessId = businessId; });
                await context.BusinessWorkingInfos.AddRangeAsync(businessWorkingInfos);
                await context.SaveChangesAsync();
                return businessWorkingInfos;
            }
        }

        public async Task<BusinessWorkingInfo> UpdateBusinessWorkingInfoAsync(BusinessWorkingInfo businessWorkingInfo)
        {
            using (var context = new AppointmentDBContext())
            {
                context.BusinessWorkingInfos.Update(businessWorkingInfo);
                await context.SaveChangesAsync();
                return businessWorkingInfo;
            }
        }
    }
}
