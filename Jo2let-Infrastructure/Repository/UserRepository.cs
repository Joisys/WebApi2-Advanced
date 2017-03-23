using Jo2let.Interface.Interface;
using Jo2let.Interface.Repository;
using Jo2let.Model;

namespace Jo2let.Infrastructure.Repository
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {

        public UserRepository(IDatabaseFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
