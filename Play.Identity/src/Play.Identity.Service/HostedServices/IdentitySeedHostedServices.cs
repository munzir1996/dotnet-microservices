
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

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private static async Task CreateRoleIfNotExistsAsync(string role, RoleManager<ApplicationUser> roleManager)
        {

        }
    }
}
