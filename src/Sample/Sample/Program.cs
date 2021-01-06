using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Registry;
using Sample.Services;
using Sample.Workers;
using System;

namespace Sample
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Setup services
                    services.AddScoped<IWithPolicyScope, WithPolicyScope>();
                    services.AddScoped<IWithoutPolicyScope, WithoutPolicyScope>();
                    services.AddScoped<IDatabaseRemover, DatabaseRemover>();
                    services.AddScoped<IDatabaseUpdater, DatabaseUpdater>();
                    services.AddScoped<IMultiplePolicyScope, MultiplePolicyScope>();
                    services.AddScoped<MyDatabaseContext>();

                    // Setup polly
                    PolicyRegistry policyRegistry = new PolicyRegistry();

                    var primaryPolicy = Policy.Handle<Exception>().RetryAsync(1);
                    policyRegistry.Add(PolicyNames.Primary, primaryPolicy);

                    var secondaryPolicy = Policy.Handle<Exception>().RetryAsync(2);
                    policyRegistry.Add(PolicyNames.Secondary, secondaryPolicy);

                    // Setup policy registry
                    services.AddSingleton<IReadOnlyPolicyRegistry<string>, PolicyRegistry>(a => policyRegistry);

                    // Add PolicyScope
                    services.AddPolicyScope();

                    // Add worker service
                    services.AddHostedService<Worker>();
                });
    }
}
