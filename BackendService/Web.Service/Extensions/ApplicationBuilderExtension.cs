using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Service.Data;
using Web.Service.DataAccess;
using Web.Service.DataAccess.Entity;

namespace Web.Service.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> MigrateDatabaseAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var appIdentityContext = scope.ServiceProvider.GetRequiredService<IdentityServerUserDbContext>())
                {
                    try
                    {
                        appIdentityContext.Database.Migrate();
                        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                        await SeedIdentityServerAsync(appIdentityContext, userManager, roleManager);
                    }
                    catch (Exception e)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }

                using (var appBaseContext = scope.ServiceProvider.GetRequiredService<DBContext>())
                {
                    try
                    {
                        appBaseContext.Database.Migrate();
                        await SeedBaseDBAsync(appBaseContext);
                    }
                    catch(Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
               
                }
            }

            return app;
        }

        private static async Task SeedIdentityServerAsync(IdentityServerUserDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            await Task.Run(() => {
                IdentityDbInitializer.Initialize(context, userManager, roleManager);
            });
        }

        private static async Task SeedBaseDBAsync(DBContext context)
        {
            await Task.Run(() => {
                BaseDbInitializer.Initialize(context);
            });
        }
    }

}
