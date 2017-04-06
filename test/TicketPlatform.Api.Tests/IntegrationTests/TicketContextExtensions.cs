using System;
using TicketPlatform.Core;
using TicketPlatform.Core.DataAccess;

namespace TicketPlatform.Api.Tests.IntegrationTests
{
    public static class TicketContextExtensions
    {
        public static void Seed(this TicketContext context)
        {
            context.AddCustomers();
            context.AddPerformances();
            context.AddTickets();
        }

        private static void AddCustomers(this TicketContext context)
        {
            context.AddRange(
                new Customer { Id = 1, FirstName = "FName1", LastName = "LName1", Role = Role.User },
                new Customer { Id = 2, FirstName = "FName2", LastName = "LName2", Role = Role.Broker },
                new Customer { Id = 3, FirstName = "FName3", LastName = "LName3", Role = Role.Admin });

            context.SaveChanges();
        }

        private static void AddPerformances(this TicketContext context)
        {
            context.AddRange(
                new Performance { Id = 1, Date = DateTime.Now.AddDays(20), Name = "Power Metall Fest" },
                new Performance { Id = 2, Date = DateTime.Now.AddDays(22), Name = "Elton John" },
                new Performance { Id = 3, Date = DateTime.Now.AddDays(23), Name = "Folk Metall Fest" },
                new Performance { Id = 4, Date = DateTime.Now.AddDays(19), Name = "Dark Metall Fest" });

            context.SaveChanges();
        }

        private static void AddTickets(this TicketContext context)
        {
            context.AddRange(
                new Ticket { Id = 1, Performance = context.Performances.Find(1), Price = 50 },
                new Ticket { Id = 2, Performance = context.Performances.Find(2), Price = 120, Buyer = context.Customers.Find(1) },
                new Ticket { Id = 3, Performance = context.Performances.Find(2), Price = 220 },
                new Ticket { Id = 4, Performance = context.Performances.Find(3), Price = 130 },
                new Ticket { Id = 5, Performance = context.Performances.Find(2), Price = 60 });

            context.SaveChanges();
        }
    }
}
