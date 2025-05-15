
using SquareAPI.Repositories;
using SquareAPI.Services;
using System.Reflection;

namespace SquareAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Allow the React frontend to access the API
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:56842")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Configure Swagger to generate XML comments
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // Define the XML filename based on the current assembly's name
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename)); // Set the path for the XML file that contains the comments
            });

            // Register the services and repositories
            builder.Services.AddScoped<ISquareService, SquareService>();
            builder.Services.AddScoped<ISquareRepository, SquareRepository>();
            builder.Services.AddSingleton<Random>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
