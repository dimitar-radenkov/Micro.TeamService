using System.Linq;
using TeamService.Models;

namespace TeamService.Data
{
    public class DbSeeder
    {
        public static void Seed(TeamDataContext context)
        {
            if (context.TeamMembers.Any() || 
                context.Teams.Any())
            {
                return;
            }

            //add members
            var dimitar = new Member { FirstName = "Dimitar", LastName = "Radenkov" };
            var alex = new Member { FirstName = "Alex", LastName = "Radenkov" };
            var nikol = new Member { FirstName = "Nikol", LastName = "Radenkov" };

            context.Members.AddRange(dimitar, alex, nikol);

            //add teams
            var team = new Team { Name = "Radenkov Team" };

            context.Teams.Add(team);

            //add members to team
            context.TeamMembers.Add(new TeamMember { Member = dimitar, Team = team }); 
            context.TeamMembers.Add(new TeamMember { Member = alex, Team = team }); 
            context.TeamMembers.Add(new TeamMember { Member = nikol, Team = team });

            context.SaveChanges();
        }
    }
}
