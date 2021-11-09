using Autofac;
using Autofac.Extensions.DependencyInjection;
using ContactManagement.Abstractions.Repositories.Query;
using ContactManagement.Abstractions.Repositories.Write;
using ContactManagement.Abstractions.Services;
using ContactManagement.Abstractions.Settings;
using ContactManagement.Core.Services;
using ContactManagement.Infrastructure.Data.Data.Mongo.Read;
using ContactManagement.Infrastructure.Data.Data.Mongo.Write;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ContactsManagement.API
{
    public class Startup
    {
        private MongoClient mongoClient;
        private MongoUrlBuilder mongoUrlBuilder;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public ILifetimeScope AutofacContainer { get; private set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
            services.AddControllers().AddXmlSerializerFormatters();

            // Set the comments path for the Swagger JSON and UI.
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contact Management API", Version = "v1" });
                c.IncludeXmlComments(xmlPath);

            });

            services.Configure<ApplicationSettings>(this.Configuration.GetSection("ApplicationSettings"));


            services.AddTransient<IContactManagementApplication, ContactManagementApplication>();
            services.AddTransient<IContactQueryHandler, ContactQueryHandler>();
            services.AddTransient<IContactQueryRepository, ContactQueryRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();

            //add httpclient
            services.AddHttpClient();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            string virtualDirectory = Configuration.GetValue<string>("ApplicationSettings:VirtualDirectory");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(virtualDirectory + "/swagger/v1/swagger.json", "Contact Management API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
