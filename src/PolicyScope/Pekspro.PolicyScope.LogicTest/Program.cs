using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pekspro.PolicyScope.LogicTest.Services;
using Pekspro.PolicyScope.LogicTest.Workers;
using Polly;
using Polly.Registry;
using System;

namespace Pekspro.PolicyScope.LogicTest.ConsoleApp
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
                    services.AddScoped<ILogic, Logic>();
                    services.AddScoped<IDummyService1, DummyService1>();
                    services.AddScoped<IDummyService2, DummyService2>();
                    services.AddScoped<IDummyService3, DummyService3>();
                    services.AddScoped<IDummyService4, DummyService4>();

                    // Setup polly
                    PolicyRegistry policyRegistry = new PolicyRegistry();

                    var asyncRetryPolicy1 = Policy.Handle<Exception>().RetryAsync(1);
                    policyRegistry.Add(PolicyNames.AsyncPolicyName, asyncRetryPolicy1);

                    var syncRetryPolicy1 = Policy.Handle<Exception>().Retry(1);
                    policyRegistry.Add(PolicyNames.SyncPolicyName, syncRetryPolicy1);

                    services.AddSingleton<IReadOnlyPolicyRegistry<string>, PolicyRegistry>(a => policyRegistry);

                    // Add PolicyScope
                    services.AddPolicyScope();

                    // Add worker service
                    services.AddHostedService<Worker>();
                });
    }
}
