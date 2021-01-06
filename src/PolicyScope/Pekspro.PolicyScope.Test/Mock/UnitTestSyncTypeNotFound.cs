using Pekspro.PolicyScope.LogicTest;
using Pekspro.PolicyScope.LogicTest.Workers;
using Pekspro.PolicyScope.Mock;
using System.Threading.Tasks;
using Xunit;

namespace Pekspro.PolicyScope.Test.Mock
{
    public class UnitTestSyncTypeNotFound
    {
        [Fact]
        public async Task TestNoAsyncPolicyFound()
        {
            // Arrange
            var policyScopeMock = PolicyScopeMock
                            .SyncPolicy(PolicyNames.SyncPolicyName)
                            .WithNoService()
                            .WithNoResult()
                            .Build();

            Logic logic = new Logic(policyScopeMock.Object);

            // Act and assert
            await Assert.ThrowsAsync<PolicyScopeMockException>(async () =>
            {
                await logic.RunWithNoResultNoServiceAsync();
            });
        }

        [Fact]
        public void TestNoSyncPolicyFound()
        {
            // Arrange
            var policyScopeMock = PolicyScopeMock
                            .AsyncPolicy(PolicyNames.AsyncPolicyName)
                            .WithNoService()
                            .WithNoResult()
                            .Build();

            Logic logic = new Logic(policyScopeMock.Object);

            // Act and assert
            Assert.Throws<PolicyScopeMockException>(() =>
            {
                logic.RunWithNoResultNoService();
            });
        }
    }
}
