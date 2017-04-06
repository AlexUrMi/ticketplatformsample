using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using idunno.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicketPlatform.Core.DataAccess;

namespace TicketPlatform.Api.Tests.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment env)
            : base(env)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // Add in memory sqlite database for EF core.
            services.AddEntityFrameworkSqlite();
            services.AddDbContext<TicketContext>(opt =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                opt.UseSqlite(connection);
            });

            // Call base service configuration.
            base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Configure basic authentication for testing purposes. This is a replacement for identity server.
            app.UseBasicAuthentication(new BasicAuthenticationOptions
            {
                Realm = "Test",
                Events = new BasicAuthenticationEvents
                {
                    OnValidateCredentials = ctx =>
                    {
                        var claims = new List<Claim> { new Claim(JwtClaimTypes.Name, ctx.Username) };

                        // Depending on user name add id and site id claims.
                        if (ctx.Username.Equals("test1", StringComparison.OrdinalIgnoreCase))
                        {
                            claims.AddRange(new[]
                            {
                                new Claim(JwtClaimTypes.Subject, "1"),
                            });
                        }
                        else if (ctx.Username.Equals("test2", StringComparison.OrdinalIgnoreCase))
                        {
                            claims.AddRange(new[]
                            {
                                new Claim(JwtClaimTypes.Subject, "2"),
                            });
                        }
                        else if (ctx.Username.Equals("test3", StringComparison.OrdinalIgnoreCase))
                        {
                            claims.AddRange(new[]
                            {
                                new Claim(JwtClaimTypes.Subject, "3"),
                            });
                        }
                        else if (ctx.Username.Equals("test4", StringComparison.OrdinalIgnoreCase))
                        {
                            claims.AddRange(new[]
                            {
                                new Claim(JwtClaimTypes.Subject, "4"),
                            });
                        }

                        // Authenticate request.
                        ctx.Ticket = new AuthenticationTicket(
                            new ClaimsPrincipal(new ClaimsIdentity(claims, ctx.Options.AuthenticationScheme)),
                            new AuthenticationProperties(),
                            ctx.Options.AuthenticationScheme);

                        return Task.CompletedTask;
                    }
                }
            });

            // Get the context from services.
            var context = app.ApplicationServices.GetRequiredService<TicketContext>();
            context.Database.EnsureCreated();

            // Populate the database with data
            context.Seed();

            // Call base middleware configuration.
            base.Configure(app, env, loggerFactory);
        }
    }
}
