using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace TicketPlatform.Api.Tests.IntegrationTests.Tickets
{
    [Collection("Api collection")]
    public partial class SearchTicketsTests
    {
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public SearchTicketsTests(ITestOutputHelper output, ApiFixture fixture)
        {
            _output = output;
            _client = fixture.AnonymousClient;
        }

        [Fact]
        public async Task Should_Return_Tickets_For_Metall_Term()
        {
            // Arrange.
            var term = "Metall";

            // Act.
            var response = await _client.GetAsync($"api/tickets?performancename={term}");

            // Assert.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var tickets = JsonConvert.DeserializeObject<Ticket[]>(await response.Content.ReadAsStringAsync());

            Assert.Equal(2, tickets.Length);

            Assert.All(tickets, t =>
            {
                _output.WriteLine(t.Performance.Name);

                Assert.Contains(term, t.Performance.Name, StringComparison.OrdinalIgnoreCase);
            });
        }
    }
}
