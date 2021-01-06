using Pekspro.PolicyScope;
using Sample.Services;
using System.Threading.Tasks;

namespace Sample.Workers
{
    public class MultiplePolicyScope : IMultiplePolicyScope
    {
        internal IPolicyScopeBuilder PolicyScopeBuilder { get; }

        public MultiplePolicyScope(IPolicyScopeBuilder policyScopeBuilder)
        {
            PolicyScopeBuilder = policyScopeBuilder;
        }

        public Task<int> UseMultipleNestedScopesAsync()
        {
            return PolicyScopeBuilder
                        .WithAsyncPolicy(PolicyNames.Primary)
                        .WithServices<MyDatabaseContext, IDatabaseUpdater>()
                        .WithResult<int>()
                        .RunAsync(async (myDatabaseContext, databaseManipulator) =>
                        {
                            await PolicyScopeBuilder
                                .WithAsyncPolicy(PolicyNames.Secondary)
                                .WithServices<MyDatabaseContext, IDatabaseRemover>()
                                .WithNoResult()
                                .RunAsync(async (myDatabaseContext2, databaseManipulator) =>
                                {
                                    await databaseManipulator.RemoveAsync(myDatabaseContext2);
                                });

                            return await databaseManipulator.UpdateAsync(myDatabaseContext);
                        });
        }

        public async Task<int> UseMultipleSerialScopesAsync()
        {
            await PolicyScopeBuilder
                .WithAsyncPolicy(PolicyNames.Secondary)
                .WithServices<MyDatabaseContext, IDatabaseRemover>()
                .WithNoResult()
                .RunAsync(async (myDatabaseContext2, databaseManipulator) =>
                {
                    await databaseManipulator.RemoveAsync(myDatabaseContext2);
                });

            return await PolicyScopeBuilder
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
