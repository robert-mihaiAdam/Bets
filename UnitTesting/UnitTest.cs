using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;
using System.Net.Http;
using System;
using Domain.Dto;
using Newtonsoft.Json;


namespace UnitTesting
{
    public class UnitTest
    {
        private readonly ITestOutputHelper _output;
        private readonly string ApiUrl;
        private readonly HttpClient client;

        public UnitTest(ITestOutputHelper output)
        {
            _output = output;
            ApiUrl = "https://localhost:7291/api/";
            client = new HttpClient();
        }

        [Fact]
        public async Task SameEntityRaceCondition()
        {
            try
            {
                //<Todo> N users modify same entity
            }
            catch (Exception ex)
            {
                _output.WriteLine(ex.ToString());
            }
        }

        [Fact]
        public async Task MeasureTimeForBetQuote()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long noBets = 0;
            string getAllBetsURL = $"{ApiUrl}Bets";
            HttpResponseMessage response = await client.GetAsync(getAllBetsURL);
            string responseBody = await response.Content.ReadAsStringAsync();
            IEnumerable<Bets> bets = JsonConvert.DeserializeObject<IEnumerable<Bets>>(responseBody);
            List<Task> requests = new();

            for (int i = 0; i <= 10000; i++)
            {
                Bets currentBet = bets.ElementAt(i);
                string getQuoteURL = $"{ApiUrl}/Bets/findBetInQuote/${currentBet.Id}";
                requests.Add(response.Content.ReadAsStringAsync());
            }

            foreach (Task t in requests)
                await t;

            TimeSpan elapsed = stopwatch.Elapsed;
            _output.WriteLine($"Querying {noBets} entities took {elapsed.TotalSeconds} seconds");
        }
    }
}