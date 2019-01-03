//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using TeamService.Clients;
//using TeamService.Data;
//using TeamService.Models;
//using TeamService.Models.BindingModels;

//namespace TeamService.Controllers
//{   
//    [ApiController]
//    public class MembersController : ControllerBase
//    {
//        private readonly ITeamRepository teamRepository;
//        private readonly IMembersRepository membersRepository;
//        private readonly ILocationClient locationClient;

//        public MembersController(
//            ITeamRepository teamRepository, 
//            IMembersRepository membersRepository,
//            ILocationClient locationClient)
//        {
//            this.teamRepository = teamRepository;
//            this.membersRepository = membersRepository;
//            this.locationClient = locationClient;
//        }

//        [HttpGet("api/teams/{teamId}/members")]
//        public async Task<IActionResult> GetMembers(Guid teamID)
//        {
//            var team = await this.teamRepository.GetByIdAsync(teamID);
//            if (team == null)
//            {
//                return this.NotFound();
//            }
              
//            return this.Ok(team.TeamMembers.Select(x => x.Member));          
//        }

//        [HttpGet("api/teams/{teamId}/members/{memberId}")]
//        public async Task<IActionResult> GetMember(Guid teamID, Guid memberId)
//        {
//            var team = await this.teamRepository.GetByIdAsync(teamID);
//            if (team == null)
//            {
//                return this.NotFound(teamID);
//            }
         
//            var teamMember = team.TeamMembers.FirstOrDefault(m => m.MemberId == memberId);
//            if (teamMember == null)
//            {
//                return this.NotFound(memberId);
//            }

//            var lastLocation = await this.locationClient.GetLatestForMemberAsync(teamMember.MemberId);
//            var locatedMember = new LocatedMember
//            {
//                ID = teamMember.MemberId,
//                FirstName = teamMember.Member.FirstName,
//                LastName = teamMember.Member.LastName,
//                LastLocation = lastLocation
//            };

//            return this.Ok(locatedMember);                         
//        }


//        [HttpPost("api/members")]
//        public async Task<IActionResult> CreateMember([FromBody]MemberBindingModel bindingModel)
//        {
//            if (!this.ModelState.IsValid)
//            {
//                return this.BadRequest(this.ModelState);
//            }

//            var member = await this.membersRepository.AddAsync(
//                bindingModel.FirstName,
//                bindingModel.LastName);

//            return this.Created("api/members", member);
//        }

//        [HttpGet("api/members")]
//        public async Task<IActionResult> GetAll()
//        {
//            return this.Ok(await this.membersRepository.GetAllAsync());
//        }

//        [HttpDelete("api/members/{memberId}")]
//        public async Task<IActionResult> Delete(Guid memberId)
//        {
//            return this.Ok(await this.membersRepository.DeleteAsync(memberId));
//        }

//        [HttpPut("api/members/{memberId}")]
//        public async Task<IActionResult> Delete(Guid memberId, [FromBody]MemberBindingModel bindingModel)
//        {
//            if (!this.ModelState.IsValid)
//            {
//                return this.BadRequest(this.ModelState);
//            }

//            var member = await this.membersRepository.UpdateAsync(
//                memberId,
//                bindingModel.FirstName, 
//                bindingModel.LastName);

//            if (member == null)
//            {
//                return this.BadRequest(memberId);
//            }

//            return this.Ok(member);
//        }
//    }
//}