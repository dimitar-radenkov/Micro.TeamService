using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamService.Models;
using TeamService.Models.ViewModels;

namespace TeamService.Data
{
    public interface ITeamRepository
    {
        Task<IEnumerable<TeamViewModel>> GetAllAsync();

        Task<Team> GetByIdAsync(Guid id);

        Task<Team> UpdateAsync(Guid id, string name, IEnumerable<Guid> members);

        Task<Team> AddAsync(string name, IEnumerable<Guid> members);

        Task<bool> DeleteAsync(Guid id);
    }
}
