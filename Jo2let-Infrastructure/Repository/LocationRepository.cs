using Jo2let.Interface.Interface;
using Jo2let.Interface.Repository;
using Jo2let.Model;

namespace Jo2let.Infrastructure.Repository
{
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(IDatabaseFactory dbFactory): base(dbFactory)
        {

        }
    }
}
