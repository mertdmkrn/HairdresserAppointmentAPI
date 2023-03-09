using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Repository.Concrete;
using HairdresserAppointmentAPI.Service.Abstract;
using HairdresserAppointmentAPI.Service.Concrete;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var currentPath = Path.Combine(AppContext.BaseDirectory.Replace("bin\\Debug\\net6.0\\", ""));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        builder.Services.AddSwaggerGen(c =>
        {
            var filePath = Path.Combine(currentPath + "HairdresserAppointmentAPI.xml");
            c.IncludeXmlComments(filePath);
        });

        builder.Services.AddSingleton<IUserRepository, UserRepository>();
        builder.Services.AddSingleton<IBusinessRepository, BusinessRepository>();
        builder.Services.AddSingleton<IRatingRepository, RatingRepository>();
        builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();

        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IBusinessService, BusinessService>();
        builder.Services.AddSingleton<IRatingService, RatingService>();
        builder.Services.AddSingleton<ICategoryService, CategoryService>();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}