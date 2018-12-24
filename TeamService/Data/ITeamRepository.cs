using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamService.Models;

namespace TeamService.Data
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllAsync();

        Task<Team> GetByIdAsync(Guid id);

        Task<bool> AddAsync(Team team);

        Task<bool> DeleteAsync(Guid id);
    }
}
