namespace Pekspro.PolicyScope.Mock
{
    internal class AsyncPolicyScopeBaseMock
    {
        public AsyncPolicyScopeBaseMock(string policyName)
        {
            PolicyName = policyName;
        }

        public string PolicyName { get; }
    }

    internal class SyncPolicyScopeBaseMock
    {
        public SyncPolicyScopeBaseMock(string policyName)
        {
            PolicyName = policyName;
        }

        public string PolicyName { get; }
    }
}
