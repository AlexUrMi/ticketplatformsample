namespace TicketPlatform.Api.Tests.IntegrationTests.Tickets
{
    public class Ticket
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public Performance Performance { get; set; }
    }
}
