using System.Threading.Tasks;

namespace TicketPlatform.Core.Services
{
    public interface ITicketService
    {
        Ticket[] SearchTickets(string performanceName);
        Ticket[] GetUserTickets(int userId);
    }
}
