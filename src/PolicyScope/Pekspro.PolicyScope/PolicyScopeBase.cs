using Polly;
using System;

namespace Pekspro.PolicyScope
{
    internal class AsyncPolicyScopeBase
    {
        protected IServiceProvider ServiceProvider { get; }
        protected IAsyncPolicy Policy { get; }

        internal AsyncPolicyScopeBase(IServiceProvider serviceProvider, IAsyncPolicy asyncPolicy)
        {
            ServiceProvider = serviceProvider;
            Policy = asyncPolicy;
        }

        internal AsyncPolicyScopeBase(AsyncPolicyScopeBase policyScopeBase)
            : this(policyScopeBase.ServiceProvider, policyScopeBase.Policy)
        {
        }
    }

    internal class SyncPolicyScopeBase
    {
        protected IServiceProvider ServiceProvider { get; }
        protected ISyncPolicy Policy { get; }

        internal SyncPolicyScopeBase(IServiceProvider serviceProvider, ISyncPolicy asyncPolicy)
        {
            ServiceProvider = serviceProvider;
            Policy = asyncPolicy;
        }

        internal SyncPolicyScopeBase(SyncPolicyScopeBase policyScopeBase)
            : this(policyScopeBase.ServiceProvider, policyScopeBase.Policy)
        {
        }
    }
}

