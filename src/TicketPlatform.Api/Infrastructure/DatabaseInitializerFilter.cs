using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Builder;
using TicketPlatform.Core.DataAccess;
using System.Linq;
using TicketPlatform.Core;

namespace TicketPlatform.Api.Infrastructure
{
    public class DatabaseInitializerFilter : IStartupFilter
    {
        private readonly TicketContext _context;
        private IApplicationLifetime _appLifetime;

        public DatabaseInitializerFilter(IApplicationLifetime appLifetime, TicketContext context)
        {
            _appLifetime = appLifetime;
            _context = context;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) => app =>
        {
            _appLifetime.ApplicationStarted.Register(() =>
            {

                if (!_context.Customers.Any())
                {
                    _context.AddRange(
                        new Customer { FirstName = "Jack", LastName = "Richer", Role = Role.User },
                        new Customer { FirstName = "John", LastName = "Wick", Role = Role.Broker },
                        new Customer { FirstName = "Charls", LastName = "Xavier", Role = Role.Admin });
                }

                if (!_context.Tickets.Any())
                {
                    _context.AddRange(
                        new Ticket { Price = 10, Performance = new Performance { Date = DateTime.Now.AddDays(50), Name = "Metallica" } },
                        new Ticket { Price = 20, Performance = new Performance { Date = DateTime.Now.AddDays(50), Name = "Heavy metall" } },
                        new Ticket { Price = 30, Performance = new Performance { Date = DateTime.Now.AddDays(50), Name = "Opera" } });
                }

                _context.SaveChanges();
            });

            next(app);
        };
    }
}
