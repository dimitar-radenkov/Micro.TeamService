using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamService.Models;

namespace TeamService.Clients
{
    public class HttpLocationClient : ILocationClient
    {
        private readonly string url;

        public HttpLocationClient(string url)
        {
            this.url = url;
        }

        public async Task<Location> AddLocationAsync(Guid memberId, Location location)
        {
            using (var httpClient = new HttpClient())
            {
                var requestBody = JsonConvert.SerializeObject(location);

                var response =  await httpClient.PostAsJsonAsync(
                    $"{this.url}/locations/{memberId}",
                    requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var responseLocation = await response.Content.ReadAsAsync<Location>();
                    return responseLocation;
                }
            }

            return null;
        }

        public async Task<Location> GetLatestForMemberAsync(Guid memberId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{this.url}/locations/latest/{memberId}");

                if (response.IsSuccessStatusCode)
                {
                    var location = await response.Content.ReadAsAsync<Location>();
                    return location;
                }
            }

            return null;
        }
    }
}
