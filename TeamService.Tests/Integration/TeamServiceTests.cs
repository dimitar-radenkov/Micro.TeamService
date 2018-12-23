namespace TeamService.Tests.Integration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using TeamService.Data;
    using TeamService.Models;

    [TestClass]
    public class TeamServiceTests
    {
        private TestServer testServer;
        private HttpClient testClient;

        [TestInitialize]
        public void Initialize()
        {
            this.testServer = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureTestServices(services =>
                    {
                        var repo = new Mock<ITeamRepository>();
                        repo
                            .Setup(x => x.GetAllAsync())
                            .ReturnsAsync(new List<Team>()
                            {
                                new Team{ Name = "1"},
                            });

                        services.AddSingleton(repo.Object);
                       
                    }));
            
            this.testClient = this.testServer.CreateClient();
        }

        [TestMethod]
        public void OnGetAllShouldReturnAllInDatabase()
        {
            //arrange && act
            var response  = this.testClient.GetAsync("/api/teams").Result;
            response.EnsureSuccessStatusCode(); 

            string raw = response.Content.ReadAsStringAsync().Result;
            var teams = JsonConvert.DeserializeObject<List<Team>>(raw);

            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            Assert.IsNotNull(teams);
            Assert.AreEqual(1, teams.Count());
        }
    }
}
