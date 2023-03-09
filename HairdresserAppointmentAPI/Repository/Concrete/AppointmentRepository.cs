using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public async Task<Appointment> GetAppointmentByIdAsync(long id)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Appointments.FindAsync(id);
            }
        }

        public async Task<IList<Appointment>> GetAppointmentsByBusinessIdAsync(int businessId, int? page, int? take)
        {
            using (var context = new AppointmentDBContext())
            {
                if (page.HasValue && take.HasValue) {
                    return await context.Appointments
                        .Where(x => x.businessId == businessId)
                        .OrderByDescending(x => x.date)
                        .Skip(page.Value * take.Value)
                        .Take(take.Value)
                        .ToListAsync();
                }

                return await context.Appointments
                    .Where(x => x.businessId == businessId)
                    .OrderByDescending(x => x.date)
                    .ToListAsync();
            }
        }

        public async Task<IList<Appointment>> GetAppointmentsByUserIdAsync(int userId, int? page, int? take)
        {
            using (var context = new AppointmentDBContext())
            {
                if (page.HasValue && take.HasValue)
                {
                    return await context.Appointments
                        .Where(x => x.userId == userId)
                        .OrderByDescending(x => x.date)
                        .Skip(page.Value * take.Value)
                        .Take(take.Value)
                        .ToListAsync();
                }

                return await context.Appointments
                    .Where(x => x.userId == userId)
                    .OrderByDescending(x => x.date)
                    .ToListAsync();
            }
        }

        public async Task<IList<Appointment>> GetAppointmentsByUserIdWithBusinessIdAsync(int userId, int businessId, int? page, int? take)
        {
            using (var context = new AppointmentDBContext())
            {
                if (page.HasValue && take.HasValue)
                {
                    return await context.Appointments
                        .Where(x => x.businessId == businessId && x.userId == userId)
                        .OrderByDescending(x => x.date)
                        .Skip(page.Value * take.Value)
                        .Take(take.Value)
                        .ToListAsync();
                }

                return await context.Appointments
                    .Where(x => x.businessId == businessId && x.userId == userId)
                    .OrderByDescending(x => x.date)
                    .ToListAsync();
            }
        }

        public async Task<IList<Appointment>> GetAppointmentsByDateAsync(DateTime? startDate, DateTime? endDate, int? page, int? take)
        {
            using (var context = new AppointmentDBContext())
            {
                if (page.HasValue && take.HasValue)
                {
                    return await context.Appointments
                        .Where(x => x.date.Value.Date >= startDate && x.date.Value.Date <= endDate)
                        .OrderByDescending(x => x.date)
                        .Skip(page.Value * take.Value)
                        .Take(take.Value)
                        .ToListAsync();
                }

                return await context.Appointments
                    .Where(x => x.date.Value.Date >= startDate && x.date.Value.Date <= endDate)
                    .OrderByDescending(x => x.date)
                    .ToListAsync();
            }
        }

        public async Task<IList<Appointment>> GetAppointmentsByDateWithBusinessIdAsync(DateTime? startDate, DateTime? endDate, int businessId, int? page, int? take)
        {
            using (var context = new AppointmentDBContext())
            {
                if (page.HasValue && take.HasValue)
                {
                    return await context.Appointments
                        .Where(x => x.date.Value.Date >= startDate && x.date.Value.Date <= endDate && x.businessId == businessId)
                        .OrderByDescending(x => x.date)
                        .Skip(page.Value * take.Value)
                        .Take(take.Value)
                        .ToListAsync();
                }

                return await context.Appointments
                    .Where(x => x.date.Value.Date >= startDate && x.date.Value.Date <= endDate && x.businessId == businessId)
                    .OrderByDescending(x => x.date)
                    .ToListAsync();
            }
        }

        public async Task<IList<Appointment>> GetAppointmentsByDateWithUserIdAsync(DateTime? startDate, DateTime? endDate, int userId, int? page, int? take)
        {
            using (var context = new AppointmentDBContext())
            {
                if (page.HasValue && take.HasValue)
                {
                    return await context.Appointments
                        .Where(x => x.date.Value.Date >= startDate && x.date.Value.Date <= endDate && x.userId == userId)
                        .OrderByDescending(x => x.date)
                        .Skip(page.Value * take.Value)
                        .Take(take.Value)
                        .ToListAsync();
                }

                return await context.Appointments
                    .Where(x => x.date.Value.Date >= startDate && x.date.Value.Date <= endDate && x.userId == userId)
                    .OrderByDescending(x => x.date)
                    .ToListAsync();
            }
        }

        public async Task<Appointment> SaveAppointmentAsync(Appointment appointment)
        {
            using (var context = new AppointmentDBContext())
            {
                appointment.createDate = DateTime.Now;
                appointment.updateDate = appointment.createDate;

                await context.Appointments.AddAsync(appointment);
                await context.SaveChangesAsync();
                return appointment;
            }
        }

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            using (var context = new AppointmentDBContext())
            {
                appointment.updateDate = DateTime.Now;

                context.Appointments.Update(appointment);
                await context.SaveChangesAsync();
                return appointment;
            }
        }

        public async Task<bool> DeleteAppointmentAsync(Appointment appointment)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Appointments.Remove(appointment);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
