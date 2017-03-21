using System.Data.Common;
using Effort;

namespace Jo2let.Data.Test.Integartion
{
    public class EffortTestBase
    {
        /// <summary>
        /// The effort connection.
        /// </summary>
        protected readonly DbConnection EffortConnection;

        /// <summary>
        /// The arms context.
        /// </summary>
        protected readonly PropertyDbContext PropertyDbTestContext;

        public EffortTestBase()
        {
            EffortConnection = DbConnectionFactory.CreateTransient();
            PropertyDbTestContext = new PropertyDbContext();
            PropertyDbTestContext.Database.CreateIfNotExists();
        }
    }
}
