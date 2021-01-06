using System.Collections.Generic;
using System.Linq;

namespace Pekspro.PolicyScope.Mock
{
    public class PolicyScopeMockBuilder
    {
        internal PolicyScopeMockBuilder()
        {
        }

        internal List<PolicyScopeMockConfiguration> Configurations = new List<PolicyScopeMockConfiguration>();

        internal PolicyScopeMockConfiguration NextConfiguration => Configurations.Last();

        public AsyncPolicyScopeServiceSelectorMockBuilder AsyncPolicy(string policyName)
        {
            Configurations.Add(new PolicyScopeMockConfiguration()
            {
                PolicyName = policyName,
                IsAsync = true
            });

            return new AsyncPolicyScopeServiceSelectorMockBuilder(this);
        }

        public SyncPolicyScopeServiceSelectorMockBuilder SyncPolicy(string policyName)
        {
            Configurations.Add(new PolicyScopeMockConfiguration()
            {
                PolicyName = policyName,
                IsAsync = false
            });

            return new SyncPolicyScopeServiceSelectorMockBuilder(this);
        }

        public PolicyScopeMock Build()
        {
            return new PolicyScopeMock(Configurations);
        }
    }
}
