using Polly;

namespace Pekspro.PolicyScope
{
    public interface IPolicyScopeBuilder
    {
        IAsyncPolicyScopeServiceSelector WithAsyncPolicy(string policyName);

        IAsyncPolicyScopeServiceSelector WithAsyncPolicy(IAsyncPolicy asyncPolicy);

        ISyncPolicyScopeServiceSelector WithSyncPolicy(string policyName);

        ISyncPolicyScopeServiceSelector WithSyncPolicy(ISyncPolicy asyncPolicy);
    }
}
