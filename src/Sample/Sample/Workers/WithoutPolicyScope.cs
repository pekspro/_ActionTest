using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Registry;
using Sample.Services;
using System.Threading.Tasks;

namespace Sample.Workers
{
    public class WithoutPolicyScope : IWithoutPolicyScope
    {
        public WithoutPolicyScope(IServiceScopeFactory serviceScopeFactory, IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            ServiceScopeFactory = serviceScopeFactory;
            PolicyRegistry = policyRegistry;
        }

        public IServiceScopeFactory ServiceScopeFactory { get; }
        
        public IReadOnlyPolicyRegistry<string> PolicyRegistry { get; }

        public async Task<int> UpdateWithoutPolicyScopeAsync()
        {
            int retValue = -1;

            var policy = PolicyRegistry.Get<IAsyncPolicy>(PolicyNames.Primary);

            await policy.ExecuteAsync(async () =>
            {
                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<MyDatabaseContext>()!;
                    var manipulator = scope.ServiceProvider.GetService<IDatabaseUpdater>()!;

                    retValue = await manipulator.UpdateAsync(context).ConfigureAwait(false);
                }
            });

            return retValue;
        }
    }
}
