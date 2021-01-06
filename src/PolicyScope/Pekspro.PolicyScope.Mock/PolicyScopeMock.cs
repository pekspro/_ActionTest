using System.Collections.Generic;

namespace Pekspro.PolicyScope.Mock
{
    public class PolicyScopeMock : PolicyScopeMockFilter
    {
        public static AsyncPolicyScopeServiceSelectorMockBuilder AsyncPolicy(string policyName)
        {
            PolicyScopeMockBuilder policyScopeMockBuilder = new PolicyScopeMockBuilder();

            return policyScopeMockBuilder.AsyncPolicy(policyName);
        }

        public static SyncPolicyScopeServiceSelectorMockBuilder SyncPolicy(string policyName)
        {
            PolicyScopeMockBuilder policyScopeMockBuilder = new PolicyScopeMockBuilder();

            return policyScopeMockBuilder.SyncPolicy(policyName);
        }

        public PolicyScopeBuilderMock Object { get; }

        internal PolicyScopeMock(List<PolicyScopeMockConfiguration> configurations)
            : base(configurations)
        {
            Object = new PolicyScopeBuilderMock(Configurations);
        }
    }
}
