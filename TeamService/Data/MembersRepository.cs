using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamService.Models;

namespace TeamService.Data
{
    public class MembersRepository : IMembersRepository
    {
        private readonly TeamDataContext db;

        public MembersRepository(TeamDataContext db)
        {
            this.db = db;
        }

        public async Task<Member> AddAsync(string firstname, string lastname)
        {
            var member = new Member
            {
                ID = Guid.NewGuid(),
                FirstName = firstname,
                LastName = lastname,
            };

            await this.db.Members.AddAsync(member);
            this.db.SaveChanges();

            return member;
        }

        public async Task<Member> UpdateAsync(Guid id, string firstname, string lastname)
        {
            var member = this.db.Members.Find(id);
            if (member == null)
            {
                return null;
            }

            member.FirstName = firstname;
            member.LastName = lastname;

            this.db.Members.Update(member);
            await this.db.SaveChangesAsync();

            return member;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            this.db.Members
                .Where(x => x.ID == id)
                .DeleteFromQuery();

            return await this.db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await this.db.Members
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Member> GetByIdAsync(Guid id)
        {
            return await this.db.Members.FindAsync(id);
        }
    }
}