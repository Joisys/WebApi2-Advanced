using System;
using Jo2let.Data;
using Jo2let.Interface.Interface;

namespace Jo2let.Infrastructure
{
    public class DatabaseFactory : IDisposable, IDatabaseFactory
    {
        private PropertyDbContext _dataContext;

        public void Dispose()
        {
            _dataContext?.Dispose();
        }

        public PropertyDbContext Get()
        {
            return _dataContext ?? (_dataContext = new PropertyDbContext());
        }
    }

}
