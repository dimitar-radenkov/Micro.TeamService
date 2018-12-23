using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamService.Models;

namespace TeamService.Data
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllAsync();
    }

    public class TeamRepository : ITeamRepository
    {
        private readonly TeamDataContext db;

        public TeamRepository(TeamDataContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await this.db.Teams.ToListAsync();
        }
    }
}
