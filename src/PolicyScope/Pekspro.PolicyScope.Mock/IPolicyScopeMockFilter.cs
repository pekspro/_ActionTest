namespace Pekspro.PolicyScope.Mock
{
    public interface IPolicyScopeMockFilter
    {
        void VerifyRun(int count);
        void VerifyRunNever();
        void VerifyRunOnce();
        IPolicyScopeMockFilter WherePolicyIsAsync();
        IPolicyScopeMockFilter WherePolicyIsSync();
        IPolicyScopeMockFilter WherePolicyNameIs(string policyName);
        IPolicyScopeMockFilter WherePolicyNameIsNot(string policyName);
    }
}