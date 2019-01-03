namespace TeamService.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TeamService.Data;
    using TeamService.Models.BindingModels;

    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private readonly ITeamRepository teamRepository;
        private readonly IMembersRepository membersRepository;

        public TeamsController(
            ITeamRepository teamRepository, 
            IMembersRepository membersRepository)
        {
            this.teamRepository = teamRepository;
            this.membersRepository = membersRepository;
        }

        [HttpGet]
        //api/teams
        public async Task<IActionResult> GetAllTeams()
        {
            return this.Ok(await this.teamRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(Guid id)
        {
            var team = await this.teamRepository.GetByIdAsync(id);
            if (team == null)
            {
                return this.NotFound();
            }

            return this.Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody]TeamBindingModel newTeam)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var team = await this.teamRepository.AddAsync(
                newTeam.Name,
                newTeam.Members);
            if (team == null)
            {
                return this.BadRequest();
            }
            
            return this.Ok(team);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            var isDeleted = await this.teamRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}