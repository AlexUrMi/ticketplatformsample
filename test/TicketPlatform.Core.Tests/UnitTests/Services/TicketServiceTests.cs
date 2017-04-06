using TicketPlatform.Core.DataAccess;
using Xunit;
using Moq;
using System.Linq;
using TicketPlatform.Core.Services;

namespace TicketPlatform.Core.Tests.UnitTests.Services
{
    public class TicketServiceTests
    {
        [Fact]
        public void Should_Return_Tickets_For_Term()
        {
            // Arrange.
            var uowMock = new Mock<IUnitOfWork>();

            var ticketsRepositoryMock = new Mock<IRepository<Ticket>>();
            ticketsRepositoryMock.Setup(r => r.Query()).Returns(
                new[]
                {
                    new Ticket { Performance = new Performance { Name = "Metallica" } },
                    new Ticket { Performance = new Performance { Name = "Heavy metall" } },
                    new Ticket { Performance = new Performance { Name = "Opera" } },
                }.AsQueryable());

            uowMock.SetupGet(u => u.TicketsRepository).Returns(ticketsRepositoryMock.Object);

            var ticketService = new TicketService(uowMock.Object);

            // Act.
            var tickets = ticketService.SearchTickets("metall");

            // Assert.
            Assert.Equal(1, tickets.Length);
        }
    }
}
