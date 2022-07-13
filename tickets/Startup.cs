using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tickets.DAL;
using tickets.Middleware;
using tickets.Validation;
using Microsoft.AspNetCore.Http;
using tickets.Filters;

namespace tickets
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
            const string connectionName = "TicketsConnection";
            services.AddDbContext<SegmentContext>(options => options.UseNpgsql(Configuration.GetConnectionString(connectionName)));

            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            })
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<SaleValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<RefundValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<SegmentValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<PassengerValidator>();
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                });


            services.AddApiVersioning();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ISegmentRepository, SegmentRepository>();

            services.AddMemoryCache();

            services.AddScoped<IValidator, Validator>();
        }

        public void Configure(IApplicationBuilder app, IMemoryCache memoryCache)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            const string sectionName = "Schemas";
            Dictionary<string, string> schemaPaths = Configuration.GetSection(sectionName).Get<Dictionary<string, string>>();
            foreach (KeyValuePair<string, string> entry in schemaPaths)
            {
                memoryCache.Set(entry.Key, new AsyncLazy<JSchema>(async () =>
                {
                    string schemaString = await File.ReadAllTextAsync(entry.Value);
                    JSchema schema = JSchema.Parse(schemaString);
                    return schema;
                }, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication));
            }
        }
    }
}
