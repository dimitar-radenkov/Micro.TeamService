namespace TeamService.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
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

        private Mock<ITeamRepository> mockTeamRepository;

        [TestInitialize]
        public void Initialize()
        {
            this.mockTeamRepository = new Mock<ITeamRepository>();

            this.testServer = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureTestServices(services =>
                    {
                        services.AddSingleton(this.mockTeamRepository.Object);
                       
                    }));
            
            this.testClient = this.testServer.CreateClient();
        }

        [TestMethod]
        public void OnGetAllShouldReturnAllInDatabase()
        {
            //arrange     
            this.mockTeamRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Team> { new Team { Name = "test team" } });

            //act
            var response  = this.testClient.GetAsync("/api/teams").Result;
            response.EnsureSuccessStatusCode(); 

            string raw = response.Content.ReadAsStringAsync().Result;
            var teams = JsonConvert.DeserializeObject<List<Team>>(raw);

            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            Assert.IsNotNull(teams);
            Assert.AreEqual(1, teams.Count());
        }

        [TestMethod]
        public void GetById_WithInvalidId_ShouldReturnNotFound()
        {
            //arrange     
            this.mockTeamRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Team)null);

            var guid = Guid.NewGuid();

            //act
            var response = this.testClient.GetAsync($"/api/teams/{guid}").Result;

            //assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
