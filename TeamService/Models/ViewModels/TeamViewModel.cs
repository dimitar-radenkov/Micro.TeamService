using System;
using System.Collections.Generic;

namespace TeamService.Models.ViewModels
{
    public class TeamViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Member> Members { get; set; }
    }
}
