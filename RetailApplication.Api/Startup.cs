using Microsoft.Extensions.DependencyInjection;
using RetailApplication.Core.IDBHelpers;
using RetailApplication.Core.RepositoryInterfaces;
using RetailApplication.Core.ServiceInterfaces;
using RetailApplication.Infrastructure.DBHelpers;
using RetailApplication.Infrastructure.Repositories;
using RetailApplication.Infrastructure.Services;
using System.Configuration;

namespace RetailApplication.Api
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
            services.AddControllers();

            // Add services to the container.
            var connectionString = Configuration.GetSection("ConnectionStrings:RetailDb");
            services.Configure<DBHelper>(Configuration.GetSection("ConnectionStrings:RetailDb"));
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IDBHelper, DBHelper>(s => new DBHelper(connectionString.Value));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductApprovalService, ProductApprovalService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //07/29
            //app.UseCors(builder =>
            //{
            //    builder.WithOrigins(Configuration.GetValue<string>("clientSPAUrl")).AllowAnyHeader()
            //        .AllowAnyMethod()
            //        .AllowCredentials();
            //});


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
