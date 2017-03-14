using System.Data.Entity;
using Jo2let.Data.Configurations;
using Jo2let.Model;

namespace Jo2let.Data
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext() : base("Jo2letDbContext")
        {
            Database.SetInitializer(new PropertyDbInitializer());
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LocationConfiguration());
            modelBuilder.Configurations.Add(new PropertyConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}