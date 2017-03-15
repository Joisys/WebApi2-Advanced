using Jo2let.Api.Models.Location;

namespace Jo2let.Api.Models.Property
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LocationViewModel Location { get; set; }

    }
}