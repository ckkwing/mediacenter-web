using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Web.Service.DataAccess;
using Web.Service.DataAccess.Entity;

namespace Web.Service.Data
{
    public static class IdentityDbInitializer
    {
        private const string adminName = "Admin";
        private const string adminRoleName = "Administrators";
        public static void Initialize(IdentityServerUserDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                InitializeUsers(userManager).GetAwaiter().GetResult();
            }
            InitalizeRoles(roleManager).GetAwaiter().GetResult();


            //if (context.Users.Any())
            //{
            //    return; //DB has been seeded
            //}


            //var user = new User { UserName = adminName };
            //var result = userManager.CreateAsync(user, "1qaz@WSX").Result;

            //result = roleManager.CreateAsync(new Role() { Name = adminRoleName}).Result;
            //result = userManager.AddToRoleAsync(user, adminRoleName).Result;



            //var users = new User[]
            //{
            //    new User(){ UserName = "Eric Chen", Department="IT", Email="test.kankan@gmail.com", PhoneNumber="13511111111", PasswordHash="999D502F-A3AB-4CD8-9416-7F8EE33BA0BE", EmailConfirmed=true}
            //};
            //foreach(User user in users)
            //{
            //    context.Users.Add(user);
            //}
            //context.SaveChanges();
        }

        private static async Task InitializeUsers(UserManager<User> userManager)
        {
            var user = new User { UserName = adminName };
            var result = await userManager.CreateAsync(user, "1qaz@WSX");
            if (result.Succeeded)
            {
                result = await userManager.AddToRoleAsync(user, adminRoleName);
            }
        }

        private static async Task InitalizeRoles(RoleManager<Role> roleManager)
        {
            if (roleManager == null)
                return;
            if (await roleManager.FindByNameAsync(adminRoleName) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = adminRoleName });
            }
        }
    }
}
