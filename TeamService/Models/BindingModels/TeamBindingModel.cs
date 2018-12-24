using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamService.Models.BindingModels
{
    public class TeamBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IEnumerable<Member> Members { get; set; }
    }
}
