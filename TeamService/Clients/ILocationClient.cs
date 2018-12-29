using System;
using System.Threading.Tasks;
using TeamService.Models;

namespace TeamService.Clients
{
    public interface ILocationClient
    {
        Task<Location> GetLatestForMemberAsync(Guid memberId);

        Task<Location> AddLocationAsync(Guid memberId, Location locationRecord);
    }
}
