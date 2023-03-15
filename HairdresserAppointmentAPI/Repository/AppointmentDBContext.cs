using HairdresserAppointmentAPI.Helpers;
using HairdresserAppointmentAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HairdresserAppointmentAPI.Repository
{
    public class AppointmentDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(HelperMethods.GetConfiguration()["ConnectionStrings:AWSPostgreSQL"], x => x.UseNetTopologySuite());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("postgis");

            builder.Entity<Business>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.name)
                    .HasMaxLength(150);

                entity.Property(e => e.city)
                    .HasMaxLength(100);

                entity.Property(e => e.province)
                    .HasMaxLength(100);

                entity.Property(e => e.district)
                    .HasMaxLength(100);

                entity.Property(e => e.telephone)
                    .HasMaxLength(11);

                entity.Property(e => e.email)
                    .HasMaxLength(100);

                entity.Property(e => e.password)
                    .HasMaxLength(100);

                entity.Property(e => e.latitude);
                entity.Property(e => e.longitude);
                entity.Property(e => e.verified);
                entity.Property(e => e.createDate);
                entity.Property(e => e.updateDate);
                entity.Property(e => e.workingType);

                entity.Property(e => e.workingStartHour)
                    .HasMaxLength(5);

                entity.Property(e => e.workingEndHour)
                    .HasMaxLength(5);

                entity.Property(e => e.appointmentTimeInterval);
                entity.Property(e => e.appointmentPeopleCount);
                entity.Property(e => e.officialHolidayAvailable);
                entity.Property(e => e.location);
            });

            builder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.id);


                entity.Property(e => e.firstName)
                    .HasMaxLength(100);

                entity.Property(e => e.lastName)
                    .HasMaxLength(100);

                entity.Property(e => e.fullName)
                    .HasMaxLength(200);

                entity.Property(e => e.email)
                    .HasMaxLength(100);

                entity.Property(e => e.password)
                    .HasMaxLength(100);

                entity.Property(e => e.createDate);
                entity.Property(e => e.updateDate);
                
                entity.Property(e => e.imagePath)
                    .HasMaxLength(100);

                entity.Property(e => e.verified);
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.name)
                    .HasMaxLength(100);
            });

            builder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.comment)
                    .HasMaxLength(300);

                entity.Property(e => e.point);
                entity.Property(e => e.businessId);
                entity.Property(e => e.userId);
                entity.Property(e => e.createDate);
                entity.Property(e => e.updateDate);

                entity.HasOne(d => d.business)
                    .WithMany(p => p.ratings)
                    .HasForeignKey(d => d.businessId)
                    .HasConstraintName("BusinessFK")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Services>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.name);
                entity.Property(e => e.price);
                entity.Property(e => e.businessId);

                entity.HasOne(d => d.business)
                    .WithMany(p => p.services)
                    .HasForeignKey(d => d.businessId)
                    .HasConstraintName("BusinessFK")
                    .OnDelete(DeleteBehavior.Cascade);
            });


            builder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.date);
                entity.Property(e => e.createDate);
                entity.Property(e => e.updateDate);

                entity.Property(e => e.description)
                    .HasMaxLength(100);

                entity.Property(e => e.status);
                entity.Property(e => e.userId);
                entity.Property(e => e.businessId);
            });

            builder.Entity<BusinessCategory>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.businessId);
                entity.Property(e => e.categoryId);

                entity.HasOne(d => d.business)
                    .WithMany(p => p.categories)
                    .HasForeignKey(d => d.businessId)
                    .HasConstraintName("BusinessFK")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.category)
                    .WithMany(p => p.categories)
                    .HasForeignKey(d => d.categoryId)
                    .HasConstraintName("CategoryFK")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<BusinessGallery>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.imagePath)
                    .HasMaxLength(150);

                entity.Property(e => e.size)
                    .HasMaxLength(10);

                entity.Property(e => e.businessId);

                entity.HasOne(d => d.business)
                    .WithMany(p => p.galleries)
                    .HasForeignKey(d => d.businessId)
                    .HasConstraintName("BusinessFK")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<BusinessWorkingInfo>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.date);

                entity.Property(e => e.startHour)
                    .HasMaxLength(5);

                entity.Property(e => e.endHour)
                    .HasMaxLength(5);

                entity.Property(e => e.appointmentTimeInterval);
                entity.Property(e => e.appointmentPeopleCount);
                entity.Property(e => e.businessId);
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<BusinessGallery> Galleries { get; set; }
        public DbSet<BusinessCategory> BusinessCategories { get; set; }
        public DbSet<BusinessWorkingInfo> BusinessWorkingInfos { get; set; }

    }
}
