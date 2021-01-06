using Pekspro.PolicyScope;
using Sample.Services;
using System.Threading.Tasks;

namespace Sample.Workers
{
    public class WithPolicyScope : IWithPolicyScope
    {
        internal IPolicyScopeBuilder PolicyScopeBuilder { get; }

        public WithPolicyScope(IPolicyScopeBuilder policyScopeBuilder)
        {
            PolicyScopeBuilder = policyScopeBuilder;
        }

        public Task<int> UpdateWithPolicyScopeAsync()
        {
            return PolicyScopeBuilder
                        .WithAsyncPolicy(PolicyNames.Primary)
                        .WithServices<MyDatabaseContext, IDatabaseUpdater>()
                        .WithResult<int>()
                        .RunAsync(async (myDatabaseContext, databaseManipulator) =>
                        {
                            return await databaseManipulator.UpdateAsync(myDatabaseContext);
                        });
        }
    }
}
