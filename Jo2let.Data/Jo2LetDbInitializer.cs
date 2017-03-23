using System;
using System.Collections.Generic;
using System.Data.Entity;
using Jo2let.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Jo2let.Data
{
    public class PropertyDbInitializer : DropCreateDatabaseAlways<PropertyDbContext>
    {
        protected override void Seed(PropertyDbContext context)
        {
            SeedUser(context);
            SeedLocationAndProperty(context);
            base.Seed(context);
        }

        private static void SeedUser(PropertyDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(new IdentityRole("User"));
            }

            var user = new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "Jose",
                Email = "admin@joisys.com",
                UserName = "admin@joisys.com"
            };

            var userResult = userManager.Create(user, "Password");

            if (userResult.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admin");
            }


        }

        private static void SeedLocationAndProperty(PropertyDbContext context)
        {
            List<Location> locationsList = new List<Location>();
            for (int i = 0; i < 5; i++)
            {
                Location location = new Location
                {
                    Name = "Location " + i,
                    Properties = new List<Property>()
                };


                int noOfProperties = new Random().Next(1, 10);
                for (int j = 0; j < noOfProperties; j++)
                {
                    location.Properties.Add(new Property
                    {
                        Title = "Property Title ",
                        Description = "Property Description",
                        Location = location
                    });
                }
                locationsList.Add(location);
            }
            context.Locations.AddRange(locationsList);
        }
    }
}