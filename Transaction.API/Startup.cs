using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.BusinessLogic.Service;
using Transaction.DataAccess;

namespace Transaction.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<TContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString(nameof(TContext)),
                b => b.MigrationsAssembly(typeof(TContext).Assembly.FullName)));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);
            services.AddSwaggerGen();
            services.AddTransient<IPersonRepo, PersonRepo>();
            services.AddTransient<IAccountRepo, AccountRepo>();
            services.AddTransient<ITransactionRepo, TransactionRepo>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Bank Transaction API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://localhost:44343/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Emmanuel Ebelebe",
                        Email = "ebelebeemmanuel@yahoo.com",
                        Url = new Uri("https://localhost:44343/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://localhost:44343/"),
                    }
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
