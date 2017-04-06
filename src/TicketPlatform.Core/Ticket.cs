namespace TicketPlatform.Core
{
    public class Ticket
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public Performance Performance { get; set; }

        public Customer Seller { get; set; }

        public Customer Buyer { get; set; }
    }
}
