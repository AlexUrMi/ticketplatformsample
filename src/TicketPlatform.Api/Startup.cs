using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using TicketPlatform.Api.Infrastructure;
using TicketPlatform.Core;
using TicketPlatform.Core.DataAccess;
using TicketPlatform.Core.Services;

namespace TicketPlatform.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "API",
                    Version = "v1"
                });

                options.DescribeAllEnumsAsStrings();
                options.DescribeStringEnumsInCamelCase();
                options.DescribeAllParametersInCamelCase();
            });

            services.AddDbContext<TicketContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("TicketPlatform"), sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                    sqlOptions.UseRowNumberForPaging();
                });
            });

            services.AddScoped<IRepository<Ticket>, TicketRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITicketService, TicketService>();

            if (!Environment.IsEnvironment("Test"))
            {
                services.AddSingleton<IStartupFilter, DatabaseInitializerFilter>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var loggerConfig = new LoggerConfiguration();

            // Add trace output in development environment.
            if (env.IsDevelopment())
            {
                loggerConfig.WriteTo.Trace();
            }

            // Read the rest part of loggers from settings file.
            loggerConfig.ReadFrom.ConfigurationSection(Configuration.GetSection("Serilog"));

            loggerFactory.AddSerilog(loggerConfig.CreateLogger());

            if (!Environment.IsEnvironment("Test"))
            {
                app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
                {
                    Authority = "http://localhost",
                    AllowedScopes = new[] { "my_scope" },
                    RequireHttpsMetadata = false,
                    EnableCaching = true
                });
            }

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.).
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });
        }
    }
}
