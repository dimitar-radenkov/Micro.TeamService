namespace TeamService.Models
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        public string Name { get; set; }
        public Guid ID { get; set; }
        public ICollection<Member> Members { get; set; }

        public override string ToString() => this.Name;
    }
}