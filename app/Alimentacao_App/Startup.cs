using Microsoft.EntityFrameworkCore;

using Alimentacao_App.DbContexts;
using Alimentacao_App.Repository;

namespace Alimentacao_App
{
     public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration {get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AnimalContext>(opt => opt.UseSqlite("Data source=AnimalAlimentacao.db"));
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(); //{c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "PetControl_Alimentação", Version = "v1"}
        }
        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
        
    }
}