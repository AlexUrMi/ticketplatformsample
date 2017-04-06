using Microsoft.EntityFrameworkCore;

namespace TicketPlatform.Core.DataAccess
{
    public class TicketContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Performance> Performances { get; set; }

        public TicketContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
