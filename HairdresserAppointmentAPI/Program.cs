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

        builder.Services.AddSwaggerGen(c =>
        {
            var filePath = Path.Combine(currentPath + "HairdresserAppointmentAPI.xml");
            c.IncludeXmlComments(filePath);
        });

        builder.Services.AddSingleton<IUserService, UserService>();

        builder.Services.AddSingleton<IUserRepository, UserRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}