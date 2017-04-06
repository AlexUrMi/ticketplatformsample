using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TicketPlatform.Core.DataAccess
{
    public class TicketRepository : Repository<Ticket>
    {
        public TicketRepository(TicketContext context) : base(context)
        {
        }

        public override IQueryable<Ticket> Query() => base.Query().Include(t => t.Performance);
    }
}
