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
        private long noEntities = 0;

        public BetableEntity_UnitTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task GetAllEntitiesAsync()
        {
            string getAllUrl = $"{ApiUrl}/BetEntity/all";

            try
            {
                HttpResponseMessage response = await client.GetAsync(getAllUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                IEnumerable<BetableEntityDto> responseObjects = JsonConvert.DeserializeObject<IEnumerable<BetableEntityDto>>(responseBody);
                noEntities = responseObjects.LongCount();
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error: {ex.Message}");
            }

        }

        [Fact]
        public async Task PostEntityAsync()
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
                _output.WriteLine($"Error: {ex.Message}");
            }
        }

        private bool compareEntities(BetableEntityDto entity1, BetableEntityDto entity2)
        {
            return entity1.Id == entity2.Id && entity1.Name == entity2.Name;  
        }

        [Fact]
        public async Task GetEntityByIdAsync()
        {
            try
            {
                Guid id = testObject.Id;
                if (id == Guid.Empty)
                {
                    throw new Exception("Test object was not created");
                }

                string getEntityUrl = $"{ApiUrl}/BetEntity/${id}";

                HttpResponseMessage response = await client.GetAsync(getEntityUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                BetableEntityDto responseObjects = JsonConvert.DeserializeObject<BetableEntityDto>(responseBody);
                Assert.True(compareEntities(responseObjects, testObject));
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error: {ex.Message}");
            }
        }



    }
}