using System.Linq;
using AutoMapper;
using TeamService.Models;
using TeamService.Models.ViewModels;

namespace TeamService.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<Team, TeamViewModel>()
                .ForMember(
                    vm => vm.Members, 
                    cfg => cfg.MapFrom(m => m.TeamMembers.Select(tm => tm.Member)));
        }
    }
}
