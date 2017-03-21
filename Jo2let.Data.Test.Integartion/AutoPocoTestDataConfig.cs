using AutoPoco;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using Jo2let.Model;

namespace Jo2let.Data.Test.Integartion
{
    public class AutoPocoTestDataConfig
    {

        /// <summary>
        /// Gets or sets the generation session
        /// </summary>
        public IGenerationSession Session { get; set; }

        public AutoPocoTestDataConfig()
        {
            var factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });
                AddPocoEntity(x);

            });

            Session = factory.CreateSession();
        }

        /// <summary>
        /// Configure the poco entity
        /// </summary>
        /// <param name="cfBuilder"></param>
        private void AddPocoEntity(IEngineConfigurationBuilder cfBuilder)
        {
            cfBuilder.AddFromAssemblyContainingType<Location>();
            cfBuilder.AddFromAssemblyContainingType<Property>();
        }

    }
}
