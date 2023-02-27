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
            optionsBuilder.UseNpgsql(HelperMethods.GetConfiguration()["ConnectionStrings:AWSPostgreSQL"], x=>x.UseNetTopologySuite());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("postgis");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }

    }
}
