using System.Collections.Generic;
using System.Linq;

namespace Pekspro.PolicyScope.Mock
{
    public class PolicyScopeMockFilter : IPolicyScopeMockFilter
    {
        internal IEnumerable<PolicyScopeMockConfiguration> Configurations { get; }

        internal PolicyScopeMockFilter(IEnumerable<PolicyScopeMockConfiguration> configurations)
        {
            Configurations = configurations;
        }

        public IPolicyScopeMockFilter WherePolicyIsAsync()
        {
            return new PolicyScopeMockFilter(Configurations.Where(c => c.IsAsync == true));
        }

        public IPolicyScopeMockFilter WherePolicyIsSync()
        {
            return new PolicyScopeMockFilter(Configurations.Where(c => c.IsAsync == false));
        }

        public IPolicyScopeMockFilter WherePolicyNameIs(string policyName)
        {
            return new PolicyScopeMockFilter(Configurations.Where(c => c.PolicyName == policyName));
        }

        public IPolicyScopeMockFilter WherePolicyNameIsNot(string policyName)
        {
            return new PolicyScopeMockFilter(Configurations.Where(c => c.PolicyName != policyName));
        }

        public void VerifyRunNever()
        {
            VerifyRun(0);
        }

        public void VerifyRunOnce()
        {
            VerifyRun(1);
        }

        public void VerifyRun(int count)
        {
            int runCount = Configurations.Sum(conf => conf.ExecutionCount);

            if (runCount != count)
            {
                throw new PolicyScopeMockException($"Expected policy scopes to be executed {count} times, but they where executed {runCount} times");
            }
        }
    }
}
