using System.Threading.Tasks;

namespace TicketPlatform.Core.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<Ticket> TicketsRepository { get; }

        Task<int> SaveChanges();
    }
}
