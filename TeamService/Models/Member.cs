namespace TeamService.Models
{
    using System;
    using System.Collections.Generic;

    public class Member
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }

        public Member()
        {
            this.TeamMembers = new List<TeamMember>();
        }

        public override string ToString() =>  $"{this.FirstName} {this.LastName}";
    }
}