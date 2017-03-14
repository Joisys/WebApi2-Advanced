using System;
using Jo2let.Data;


namespace Jo2let.Interface.Interface 
{
    public interface IDatabaseFactory : IDisposable
    {
        PropertyDbContext Get();
    }
}
