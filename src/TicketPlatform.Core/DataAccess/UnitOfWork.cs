using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TicketPlatform.Core.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(IRepository<Ticket> ticketsRepository, TicketContext context)
        {
            TicketsRepository = ticketsRepository;
            _context = context;
        }

        public IRepository<Ticket> TicketsRepository { get; }

        public async Task<int> SaveChanges() => await _context.SaveChangesAsync();
    }
}
