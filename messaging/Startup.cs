using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Messaging.Intraestructure.MySql;
using Messaging.Application.Services;
using Messaging.Domain;
using DotNetEnv;
namespace Messaging.Intraestructure
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MessageAppDbContext>(options =>
            {
                
                Env.Load("../.env");
                string server = Environment.GetEnvironmentVariable("MYSQL_SERVER");
                string database = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
                string uid = Environment.GetEnvironmentVariable("MYSQL_USER");
                string password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
                string connectionString = $"server={server};database={database};user={uid};password={password};";
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped<MessageRepository, MySqlMessageRepository>();

            services.AddScoped<MessageService>();

            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
