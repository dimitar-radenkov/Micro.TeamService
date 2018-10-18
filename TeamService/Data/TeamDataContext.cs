namespace TeamService.Data
{
    using Microsoft.EntityFrameworkCore;
    using TeamService.Models;

    public class TeamDataContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Member> Members { get; set; }

        public TeamDataContext(DbContextOptions options) : 
            base(options)
        {

        }
    }
}
