namespace TeamService.Data
{
    using Microsoft.EntityFrameworkCore;
    using TeamService.Models;

    public class TeamDataContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

        public TeamDataContext(DbContextOptions options) : 
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamMember>()
                .HasKey(tm => new { tm.TeamId, tm.MemberId });
        }
    }
}
