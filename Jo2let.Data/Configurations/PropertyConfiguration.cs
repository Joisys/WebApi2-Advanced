using System.Data.Entity.ModelConfiguration;
using Jo2let.Model;

namespace Jo2let.Data.Configurations
{
    internal class PropertyConfiguration : EntityTypeConfiguration<Property>
    {
        public PropertyConfiguration()
        {
            HasRequired(r => r.Location).WithMany().HasForeignKey(r => r.LocationId).WillCascadeOnDelete(true);
        }

    }
}
