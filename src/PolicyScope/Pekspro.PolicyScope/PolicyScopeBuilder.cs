using Polly;
using Polly.Registry;
using System;

namespace Pekspro.PolicyScope
{
    public class PolicyScopeBuilder : IPolicyScopeBuilder
    {
        public IServiceProvider ServiceProvider { get; }

        public IReadOnlyPolicyRegistry<string> PolicyRegistry { get; }

        public PolicyScopeBuilder(IServiceProvider serviceProvider, IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            ServiceProvider = serviceProvider;
            PolicyRegistry = policyRegistry;
        }

        public IAsyncPolicyScopeServiceSelector WithAsyncPolicy(string policyName)
        {
            var policy = PolicyRegistry.Get<IAsyncPolicy>(policyName);

            return WithAsyncPolicy(policy);
        }

        public IAsyncPolicyScopeServiceSelector WithAsyncPolicy(IAsyncPolicy asyncPolicy)
        {
            return new AsyncPolicyScopeServiceSelector(ServiceProvider, asyncPolicy);
        }

        public ISyncPolicyScopeServiceSelector WithSyncPolicy(string policyName)
        {
            var policy = PolicyRegistry.Get<ISyncPolicy>(policyName);

            return WithSyncPolicy(policy);
        }

        public ISyncPolicyScopeServiceSelector WithSyncPolicy(ISyncPolicy syncPolicy)
        {
            return new SyncPolicyScopeServiceSelector(ServiceProvider, syncPolicy);
        }
    }
}
