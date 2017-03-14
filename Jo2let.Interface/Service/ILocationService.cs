using System.Collections.Generic;
using Jo2let.Model;

namespace Jo2let.Service
{
    public interface ILocationService
    {
        IEnumerable<Location> GetLocations();
        Location AddLocation(Location location);
        Location GetLocationById(int id);
        Location UpdateLoaction(Location location);
        void DeleteLocation(int locationId);

    }
}