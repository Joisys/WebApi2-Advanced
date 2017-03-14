using System.Collections.Generic;
using Jo2let.Model;

namespace Jo2let.Service
{
    public interface IPropertyService
    {
        Property AddProperty(Property property);
        IEnumerable<Property> GetAllPropertys();

        Property GetPropertyById(int id);

        Property UpdateProperty(Property property);

        void DeleteProperty(int id);

    }
}