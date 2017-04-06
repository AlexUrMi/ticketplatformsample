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
    public class GetUserTicketsTests
    {
        private readonly HttpClient _anonymousClient;
        private readonly ITestOutputHelper _output;
        private readonly HttpClient _client;

        public GetUserTicketsTests(ITestOutputHelper output, ApiFixture fixture)
        {
            _output = output;
            _anonymousClient = fixture.AnonymousClient;
            _client = fixture.Client;
        }

        [Fact]
        public async Task Should_Return_Unauthorized_For_Anonymous_User()
        {
            // Act.
            var response = await _anonymousClient.GetAsync("api/customer/me/tickets");

            // Assert.
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Tickets_For_Authorized_User()
        {
            // Act.
            var response = await _client.GetAsync("api/customer/me/tickets");

            // Assert.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var tickets = JsonConvert.DeserializeObject<Ticket[]>(await response.Content.ReadAsStringAsync());

            Assert.Equal(1, tickets.Length);

            Assert.All(tickets, t =>
            {
                _output.WriteLine(t.Performance.Name);
            });
        }
    }
}
