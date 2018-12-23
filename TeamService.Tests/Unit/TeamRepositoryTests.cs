namespace TeamService.Tests.Unit
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TeamService.Data;
    using TeamService.Models;

    [TestClass]
    public class TeamRepositoryTests
    {
        [TestMethod]
        public void SimpleTest()
        {
            //arrange
            var optionsBuilder = new DbContextOptionsBuilder<TeamDataContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new TeamDataContext(optionsBuilder.Options);

            var team = new Team { ID = Guid.NewGuid(), Name = "Test team" };
            db.Teams.Add(team);
            db.SaveChanges();

            var teamRepository = new TeamRepository(db);

            //act
            var retrievedTeam = teamRepository
                .GetAllAsync().Result
                .FirstOrDefault();

            //assert
            Assert.IsNotNull(retrievedTeam);
            Assert.AreEqual(team.ID, retrievedTeam.ID);
        }
    }
}
