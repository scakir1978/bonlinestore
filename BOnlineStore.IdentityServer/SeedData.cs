using System.Security.Claims;
using IdentityModel;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using BOnlineStore.Shared;

namespace BOnlineStore.IdentityServer;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            #region ConfigurationDbContext ile ilgili kayıtlar eklenir.

            /*var configrationDbContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            configrationDbContext.Database.Migrate();

            foreach (var client in Config.Clients)
            {
                configrationDbContext.Clients.Add(client.ToEntity());
            }*/
            

            #endregion



            #region ApplicationDbContext ile ilgili kayıtlar eklenir

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!userMgr.Users.Any())
            {
                var defaultTenantId = Guid.NewGuid();

                var adminUser = new ApplicationUser
                {
                    UserName = "administrator",
                    Email = "scakir1978@hotmail.com",
                    TenantId = defaultTenantId
                };

                var result = userMgr.CreateAsync(adminUser, "Scag185489*").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(adminUser, new Claim[]{
                            new Claim(BOnlineStoreIdentityServerConstants.ApiScopesDefinitionsTenantId, defaultTenantId.ToString()),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("administrator created");

                #endregion
            }
        }
    }
}
