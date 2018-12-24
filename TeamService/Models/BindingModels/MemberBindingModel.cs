using System.ComponentModel.DataAnnotations;

namespace TeamService.Models.BindingModels
{
    public class MemberBindingModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
