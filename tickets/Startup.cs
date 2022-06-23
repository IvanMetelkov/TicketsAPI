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
            const string nameOfConnection = "TicketsConnection";
            services.AddDbContext<SegmentContext>(options => options.UseNpgsql(Configuration.GetConnectionString(nameOfConnection)));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            }); ;

            services.AddApiVersioning();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ISegmentRepository, SegmentRepository>();

            services.AddMemoryCache();

            services.AddSingleton<IValidator>(v => ActivatorUtilities.CreateInstance<Validator>(v, Configuration.GetSection("Schemas").Get<Dictionary<string, string>>()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, IMemoryCache memoryCache*/)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }*/

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            /*Dictionary<string, string> schemaPaths = Configuration.GetSection("Schemas").Get<Dictionary<string, string>>();
            foreach (KeyValuePair<string, string> entry in schemaPaths)
            {
                if (!memoryCache.TryGetValue(entry.Key, out JSchema _))
                {
                    string schemaString = File.ReadAllText(entry.Value);
                    JSchema schema = JSchema.Parse(schemaString);
                    memoryCache.Set(entry.Key, schema);
                }
            }*/
        }
    }
}
