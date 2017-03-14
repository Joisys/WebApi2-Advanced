using Jo2let.Data;
using Jo2let.Interface.Interface;

namespace Jo2let.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PropertyDbContext _dbContext;
        private readonly IDatabaseFactory _dbFactory;
        protected PropertyDbContext DbContext => _dbContext ?? _dbFactory.Get();

        public UnitOfWork(IDatabaseFactory dbFactory, PropertyDbContext dbContext)
        {
            _dbFactory = dbFactory;
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
