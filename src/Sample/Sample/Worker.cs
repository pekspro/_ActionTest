using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Workers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample
{
    public class Worker : BackgroundService
    {
        public Worker(IServiceScopeFactory serviceScopeFactory, IHostApplicationLifetime hostApplicationLifetime)
        {
            ServiceScopeFactory = serviceScopeFactory;
            HostApplicationLifetime = hostApplicationLifetime;
        }

        public IServiceScopeFactory ServiceScopeFactory { get; }

        public IHostApplicationLifetime HostApplicationLifetime { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(100);

            Console.WriteLine();

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                IWithPolicyScope withPolicyScope = scope.ServiceProvider.GetService<IWithPolicyScope>();
                Console.WriteLine($"Running {nameof(withPolicyScope.UpdateWithPolicyScopeAsync)}...");
                await withPolicyScope.UpdateWithPolicyScopeAsync();

                IWithoutPolicyScope withoutPolicyScope = scope.ServiceProvider.GetService<IWithoutPolicyScope>();
                Console.WriteLine($"Running {nameof(withoutPolicyScope.UpdateWithoutPolicyScopeAsync)}...");
                await withoutPolicyScope.UpdateWithoutPolicyScopeAsync();

                IMultiplePolicyScope multiplePolicyScope = scope.ServiceProvider.GetService<IMultiplePolicyScope>();
                Console.WriteLine($"Running {nameof(multiplePolicyScope.UseMultipleNestedScopesAsync)}...");
                await multiplePolicyScope.UseMultipleNestedScopesAsync();
            }

            Console.WriteLine();

            HostApplicationLifetime.StopApplication();
        }
    }
}
