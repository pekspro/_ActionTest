using Moq;
using Pekspro.PolicyScope.Mock;
using Sample.Services;
using Sample.Workers;
using System.Threading.Tasks;
using Xunit;

namespace Sample.Test.Workers
{
    public class UnitTestWithPolicyScope
    {
        [Fact]
        public async Task TestUpdateWithPolicyScopeAsync()
        {
            // Arrange
            MyDatabaseContext myDatabaseContext = new MyDatabaseContext();
            Mock<IDatabaseUpdater> databaseManipulator = new Mock<IDatabaseUpdater>();

            databaseManipulator
                .Setup(d => d.UpdateAsync(myDatabaseContext))
                .Returns(Task.FromResult(42));

            PolicyScopeMock policyScopeMock = PolicyScopeMock
                            .AsyncPolicy(PolicyNames.Primary)
                            .WithServices(myDatabaseContext, databaseManipulator.Object)
                            .WithResultType<int>()
                            .Build();

            WithPolicyScope logic = new WithPolicyScope(policyScopeMock.Object);

            // Act
            int result = await logic.UpdateWithPolicyScopeAsync();

            // Assert
            policyScopeMock.VerifyRunOnce();
            Assert.Equal(42, result);
        }
    }
}
