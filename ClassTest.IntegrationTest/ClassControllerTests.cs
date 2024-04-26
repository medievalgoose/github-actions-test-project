using ClassTest.API.Models;
using ClassTest.IntegrationTest.Helpers;
using FluentAssertions;
using Newtonsoft.Json;

namespace ClassTest.IntegrationTest
{
    public class ClassControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private HttpClient _client;

        public ClassControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetClass_ValidRequest_ReturnClassList()
        {
            // Act
            var body = await _client.GetAsync("/api/Class");
            var content = await body.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Classe>>(content);

            // Assert
            result.First().ClassName.Should().Contain("Class 2");
        }
    }
}