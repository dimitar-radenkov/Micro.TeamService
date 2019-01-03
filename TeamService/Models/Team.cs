namespace TeamService.Models
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }

        public Team()
        {
            this.TeamMembers = new List<TeamMember>();
        }

        public override string ToString() => this.Name;
    }
}