using HairdresserAppointmentAPI.Helpers;
using HairdresserAppointmentAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HairdresserAppointmentAPI.Repository
{
    public class AppointmentDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(HelperMethods.GetConfiguration()["ConnectionStrings:AWSPostgreSQL"]);
        }

        public DbSet<User> Users { get; set; }

    }
}
