using Jo2let.Interface.Interface;
using Jo2let.Interface.Repository;
using Jo2let.Model;

namespace Jo2let.Infrastructure.Repository
{
    public class PropertyRepository : RepositoryBase<Property>, IPropertyRepository
    {
        public PropertyRepository(IDatabaseFactory dbFactory): base(dbFactory)
        {

        }
    }
}
