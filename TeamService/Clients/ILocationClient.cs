using System;
using System.Threading.Tasks;
using TeamService.Models;

namespace TeamService.Clients
{
    public interface ILocationClient
    {
        Task<Location> GetLatestForMember(Guid memberId);

        Task<Location> AddLocation(Guid memberId, Location locationRecord);
    }
}
