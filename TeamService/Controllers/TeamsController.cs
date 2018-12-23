namespace TeamService.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TeamService.Data;

    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private readonly ITeamRepository teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        [HttpGet]
        //api/teams
        public async Task<IActionResult> GetAllTeams()
        {
            return this.Ok(await this.teamRepository.GetAllAsync());
        }

        //[HttpGet("{id}")]
        //public IActionResult GetTeam(Guid id)
        //{
        //    var team = this.db.Teams.Find(id);

        //    if (team == null)
        //    {
        //        return this.NotFound();
        //    }

        //    return this.Ok(team);
        //}

        //[HttpPost]
        //public virtual IActionResult CreateTeam([FromBody]Team newTeam)
        //{
        //    this.db.Teams.Add(newTeam);
        //    this.db.SaveChanges();

        //    //TODO: add test that asserts result is a 201 pointing to URL of the created team.
        //    //TODO: teams need IDs
        //    //TODO: return created at route to point to team details			
        //    return this.Created($"/teams/{newTeam.ID}", newTeam);
        //}

        //[HttpPut("{id}")]
        //public virtual IActionResult UpdateTeam([FromBody]Team team, Guid id)
        //{
        //    team.ID = id;

        //    if (this.db.Update(team) == null)
        //    {
        //        return this.NotFound();
        //    }


        //    this.db.SaveChanges();
        //    return this.Ok(team);
            
        //}

        //[HttpDelete("{id}")]
        //public virtual IActionResult DeleteTeam(Guid id)
        //{
        //    var team = this.db.Teams.Find(id);         
        //    if (team == null)
        //    {
        //        return this.NotFound();
        //    }

        //    this.db.Teams.Remove(team);
        //    this.db.SaveChanges();

        //    return this.Ok(team.ID);         
        //}
    }
}