using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicketPlatform.Core.DataAccess;

namespace TicketPlatform.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Ticket[] SearchTickets(string performanceName)
        {
            if (string.IsNullOrEmpty(performanceName)) throw new ArgumentNullException(nameof(performanceName));

            var tickets = _unitOfWork.TicketsRepository.Query().Where(t => t.Performance.Name.Contains(performanceName));

            return tickets.ToArray();
        }

        public Ticket[] GetUserTickets(int userId)
        {
            var tickets = _unitOfWork.TicketsRepository.Query().Where(t => t.Buyer.Id == userId);

            return tickets.ToArray();
        }
    }
}
