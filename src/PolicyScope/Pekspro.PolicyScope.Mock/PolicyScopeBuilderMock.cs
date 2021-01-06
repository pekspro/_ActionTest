using Polly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pekspro.PolicyScope.Mock
{
    public class PolicyScopeBuilderMock : IPolicyScopeBuilder
    {
        internal PolicyScopeBuilderMock(IEnumerable<PolicyScopeMockConfiguration> configurations)
        {
            Configurations = configurations;
        }

        internal IEnumerable<PolicyScopeMockConfiguration> Configurations { get; }


        public IAsyncPolicyScopeServiceSelector WithAsyncPolicy(string policyName)
        {
            var configurations = Configurations
                                    .Where(conf => conf.IsAsync == true);

            if (!configurations.Any())
            {
                string errorMessage = $"Found no configurations with async policy.";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            configurations = configurations
                                .Where(conf => conf.PolicyName == policyName);

            if (!configurations.Any())
            {
                string errorMessage = $"Found no configurations with async policy with name {policyName}.";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new AsyncPolicyScopeServiceSelectorMock(configurations);
        }

        public IAsyncPolicyScopeServiceSelector WithAsyncPolicy(IAsyncPolicy asyncPolicy)
        {
            throw new NotImplementedException();
        }

        public ISyncPolicyScopeServiceSelector WithSyncPolicy(string policyName)
        {
            var configurations = Configurations
                                    .Where(conf => conf.IsAsync == false);

            if (!configurations.Any())
            {
                string errorMessage = $"Found no configurations with sync policy.";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            configurations = configurations
                                .Where(conf => conf.PolicyName == policyName);

            if (!configurations.Any())
            {
                string errorMessage = $"Found no configurations with sync policy with name {policyName}.";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new SyncPolicyScopeServiceSelectorMock(configurations);
        }

        public ISyncPolicyScopeServiceSelector WithSyncPolicy(ISyncPolicy syncPolicy)
        {
            throw new NotImplementedException();
        }
    }
}
