using Microsoft.AspNet.Identity.EntityFramework;

namespace Jo2let.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
