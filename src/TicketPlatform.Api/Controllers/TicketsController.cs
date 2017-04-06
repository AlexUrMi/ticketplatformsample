using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TicketPlatform.Core.Services;
using TicketPlatform.Core;
using Microsoft.AspNetCore.Authorization;
using IdentityModel;

namespace TicketPlatform.Api.Controllers
{
    [Route("api")]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Route("[controller]")]
        [HttpGet]
        public IActionResult SearchTicketsByName(string performanceName)
        {
            var tickets = _ticketService.SearchTickets(performanceName);
            return Ok(tickets);
        }

        [Route("customer/me/[controller]")]
        [HttpGet]
        [Authorize]
        public IActionResult GetUserTickets()
        {
            // Get user id from claims.
            var id = int.Parse(User.FindFirst(JwtClaimTypes.Subject).Value);

            var tickets = _ticketService.GetUserTickets(id);
            return Ok(tickets);
        }
    }
}
