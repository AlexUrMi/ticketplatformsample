using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TicketPlatform.Api.Tests.IntegrationTests.SystemTime
{
    [Collection("Api collection")]
    public class GetSystemTimeTests
    {
        private readonly HttpClient _client;

        public GetSystemTimeTests(ApiFixture fixture)
        {
            _client = fixture.AnonymousClient;
        }

        [Fact]
        public async Task Should_Return_Current_Time()
        {
            // Arrange.

            // Act.
            var response = await _client.GetAsync("api/systemtime");

            // Assert.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var time = JsonConvert.DeserializeObject<DateTime>(await response.Content.ReadAsStringAsync());

            Assert.InRange(time, DateTime.Now.AddSeconds(-1), DateTime.Now.AddSeconds(1));
        }
    }
}
