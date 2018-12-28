using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamService.Models;

namespace TeamService.Data
{
    public class TeamRepository : ITeamRepository
    {
        private readonly TeamDataContext db;

        public TeamRepository(TeamDataContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddAsync(Team team)
        {
            await this.db.Members.AddRangeAsync(team.Members);
            await this.db.Teams.AddAsync(team);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var team = await this.db.Teams.FindAsync(id);
            if (team == null)
            {
                return false;
            }

            this.db.Members
                .Where(m => team.Members.Select(tm => tm.ID).Contains( m.ID))
                .DeleteFromQuery();
            this.db.Teams.Remove(team);
            this.db.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await this.db.Teams
                .Include(x => x.Members)
                .ToListAsync();
        }

        public async Task<Team> GetByIdAsync(Guid id)
        {
            return await this.db.Teams
                .Include(x=> x.Members)
                .FirstOrDefaultAsync(t => t.ID == id);
        }
    }
}