using Xunit;

namespace TicketPlatform.Api.Tests.IntegrationTests
{
    [CollectionDefinition("Api collection")]
    public class ApiCollection : ICollectionFixture<ApiFixture>
    {
    }
}
