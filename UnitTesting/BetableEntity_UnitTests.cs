using Xunit.Abstractions;
using Domain.Dto.BetableEntity;
using System.Text;
using Newtonsoft.Json;

namespace UnitTesting
{
    public class BetableEntity_UnitTests
    {

        private readonly ITestOutputHelper _output;
        private readonly string ApiUrl = "https://localhost:7291/api";
        static readonly HttpClient client = new HttpClient();
        private BetableEntityDto testObject;

        public BetableEntity_UnitTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Get_All_Entities()
        {
            
        }

        [Fact]
        public async Task Post_Entity()
        {
            CreateBetableEntityDto createBetableEntityDto = new CreateBetableEntityDto { Name = $"BetableEntity {Guid.NewGuid()}" };
            var body = System.Text.Json.JsonSerializer.Serialize(createBetableEntityDto);
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            string postUrl = $"{ApiUrl}/BetEntity";

            try
            {
                HttpResponseMessage response = await client.PostAsync(postUrl, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                testObject = JsonConvert.DeserializeObject<BetableEntityDto>(responseBody);
                Assert.True((testObject.Id != Guid.Empty && testObject.Name == createBetableEntityDto.Name));
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Message: {ex.Message}");
                Assert.True(false);
            }
        }



    }
}