namespace TeamService.Tests
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TeamService.Controllers;
    using TeamService.Data;
    using TeamService.Models;

    [TestClass]
    public class TeamControllerTests
    {
        [TestMethod]
        public void CreateTeam_ShouldAddTeamToDatabase()
        {
            //arrange
            var optionsBuilder = new DbContextOptionsBuilder<TeamDataContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var db = new TeamDataContext(optionsBuilder.Options);
            var controller = new TeamsController(db);

            //act
            var team = new Team { ID = Guid.NewGuid(), Name = "Test" };
            var result = controller.CreateTeam(team) as CreatedResult ;

            //assert
            Assert.AreEqual(result.StatusCode, 201);
            Assert.IsNotNull(db.Teams.Find(team.ID));
        }
    }
}
