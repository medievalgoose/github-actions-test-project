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
        public async Task GetClasses_ValidRequest_ReturnClassList()
        {
            // Act
            var body = await _client.GetAsync("/api/Class");
            var content = await body.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Classe>>(content);

            // Assert
            result.First().ClassName.Should().Contain("Class 2");
        }

        [Fact]
        public async Task GetClass_ValidId_ReturnRelevantClass()
        {
            // Arrange
            Classe expectedClass = new()
            {
                Id = "70c0b14a-64db-47",
                ClassName = "Class 1",
                Participant = 5
            };

            string id = "70c0b14a-64db-47";

            // Act
            var body = await _client.GetAsync($"/api/Class/{id}");
            var content = await body.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Classe>(content);

            // Assert
            result.Should().BeEquivalentTo(expectedClass);
        }
    }
}