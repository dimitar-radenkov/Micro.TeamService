using System;

namespace TeamService.Models
{
    public class TeamMember
    {
        public Guid TeamId { get; set; }
        public Team Team { get; set; }

        public Guid MemberId { get; set; }
        public Member Member { get; set; }
    }
}
