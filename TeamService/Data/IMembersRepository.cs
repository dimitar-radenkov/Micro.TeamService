using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamService.Models;

namespace TeamService.Data
{
    public interface IMembersRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();

        Task<Member> GetByIdAsync(Guid id);

        Task<Member> AddAsync(string firsname, string lastname);

        Task<Member> UpdateAsync(Guid id, string firstname, string lastname);

        Task<bool> DeleteAsync(Guid id);
    }
}