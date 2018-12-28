using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamService.Clients;
using TeamService.Data;
using TeamService.Models;

namespace TeamService.Controllers
{
    [Route("api/teams/{teamId}/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ITeamRepository teamRepository;
        private readonly ILocationClient locationClient;

        public MembersController(
            ITeamRepository teamRepository, 
            ILocationClient locationClient)
        {
            this.teamRepository = teamRepository;
            this.locationClient = locationClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers(Guid teamID)
        {
            var team = await teamRepository.GetByIdAsync(teamID);

            if (team == null)
            {
                return this.NotFound();
            }
              
            return this.Ok(team.Members);          
        }

        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetMember(Guid teamID, Guid memberId)
        {
            var team = await this.teamRepository.GetByIdAsync(teamID);

            if (team == null)
            {
                return this.NotFound();
            }

            
            var member = team.Members.FirstOrDefault(m => m.ID == memberId);
            if (member == null)
            {
                return this.NotFound();
            }

            var lastLocation = await this.locationClient.GetLatestForMember(member.ID);
            var locatedMember = new LocatedMember
            {
                ID = member.ID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                LastLocation = lastLocation
            };

            return this.Ok(locatedMember);                         
        }
    }
}