using OpenTriviaApp.API.Services;

namespace OpenTriviaApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient();

            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddScoped<ITriviaService, TriviaService>();

            var app = builder.Build();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
