
using StudentCenterEmailApi.src.Application.Interfaces;
using StudentCenterEmailApi.src.Application.Services;
using StudentCenterEmailApi.src.RabbitMqClient;

namespace StudentCenterEmailApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddHostedService<RabbitMqSubscriber>();

            builder.Services.AddSingleton<IProcessEvent, ProcessEvent>();

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(80);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
