namespace SubtitleCommunitySystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using SubtitleCommunitySystem.Model;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;       
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Data.ApplicationDbContext context)
        {
            if (context.Roles.Count() > 0)
            {
                return;
            }

            this.AddRole(context, "Admin");
            this.AddInitialAdmin(context);

            this.AddRole(context, "Moderator");
            this.AddRole(context, "Writer");
            this.AddRole(context, "Translator");
            this.AddRole(context, "Sync");
            this.AddRole(context, "ImageManager");
            this.AddRole(context, "Revisioner");

            this.AddLanguages(context);
        }

        private void AddInitialAdmin(ApplicationDbContext context)
        {
            string username = "admin@gmail.com";
            string password = "123456";
            var admin = new ApplicationUser()
            {
                UserName = username,
                Email = username
            };

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!userManager.Users.Any(u => u.UserName == username))
            {
                userManager.Create(admin, password);
                userManager.AddToRole(admin.Id, "Admin");
            }
        }

        private void AddRole(ApplicationDbContext context, string roleName = "Admin")
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        private void AddLanguages(ApplicationDbContext context)
        {
            context.Languages.Add(new Language()
            {
                Name = "English",
            });

            context.Languages.Add(new Language()
            {
                Name = "Italian",
            });

            context.Languages.Add(new Language()
            {
                Name = "Bulgarian",
            });

            context.SaveChanges();
        }
    }
}
