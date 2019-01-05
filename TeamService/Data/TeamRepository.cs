using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeamService.Models;
using TeamService.Models.ViewModels;

namespace TeamService.Data
{
    public class TeamRepository : ITeamRepository
    {
        private readonly TeamDataContext db;
        private readonly IMapper mapper;

        public TeamRepository(TeamDataContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Team> AddAsync(string name, IEnumerable<Guid> members)
        {
            if(!members.All(x => this.db.Members.Find(x) != null))
            {
                //members does not persist in the db
                return null;
            }

            var team = new Team
            {
                ID = Guid.NewGuid(),
                Name = name,
                TeamMembers = members
                    .Select(id => new TeamMember { MemberId = id })
                    .ToList()
            };

            await this.db.Teams.AddAsync(team);
            this.db.SaveChanges();

            return team;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var team = await this.db.Teams.FindAsync(id);
            if (team == null)
            {
                return false;
            }

            this.db.Members
                .Where(m => team.TeamMembers.Select(tm => tm.MemberId).Contains( m.ID))
                .DeleteFromQuery();
            this.db.Teams.Remove(team);
            this.db.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<TeamViewModel>> GetAllAsync()
        {
            return await this.db.Teams
                .Include(x => x.TeamMembers)
                .Select(x => this.mapper.Map<Team, TeamViewModel>(x))
                .ToListAsync();
        }

        public async Task<Team> GetByIdAsync(Guid id)
        {
            return await this.db.Teams
                .Include(x=> x.TeamMembers)
                .FirstOrDefaultAsync(t => t.ID == id);
        }

        public Task<Team> UpdateAsync(Guid id, string name, IEnumerable<Guid> members)
        {
            throw new NotImplementedException();
        }
    }
}