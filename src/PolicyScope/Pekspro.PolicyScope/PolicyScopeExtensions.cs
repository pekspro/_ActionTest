using Pekspro.PolicyScope;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PolicyScopeExtensions
    {
        public static IServiceCollection AddPolicyScope(this IServiceCollection services)
        {
            services.AddScoped<IPolicyScopeBuilder, PolicyScopeBuilder>();

            return services;
        }
    }
}
