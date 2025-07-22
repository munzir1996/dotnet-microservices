
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Play.Identity.Service.Entities;
using Play.Identity.Service.Settings;

namespace Play.Identity.Service.HostedServices
{
    public class IdentitySeedHostedServices : IHostedService
    {

        private readonly IServiceScopeFactory _servicescopeFactory;
        private readonly IdentitySettings _identitySettings;

        public IdentitySeedHostedServices(IServiceScopeFactory serviceScopeFactory, IOptions<IdentitySettings> identityOptions)
        {
            this._servicescopeFactory = serviceScopeFactory;
            _identitySettings = identityOptions.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _servicescopeFactory.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await CreateRoleIfNotExistsAsync(Roles.Admin, roleManager);
            await CreateRoleIfNotExistsAsync(Roles.Player, roleManager);

            var adminUser = await userManager.FindByEmailAsync(_identitySettings.AdminUserEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {

                    UserName = _identitySettings.AdminUserEmail,
                    Email = _identitySettings.AdminUserEmail,
                };
                await userManager.CreateAsync(adminUser, _identitySettings.AdminUserPassword);
                await userManager.AddToRoleAsync(adminUser, Roles.Admin);
            }

        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private static async Task CreateRoleIfNotExistsAsync(string role, RoleManager<ApplicationRole> roleManager)
        {

            var roleExists = await roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = role });
            }

        }
    }
}
