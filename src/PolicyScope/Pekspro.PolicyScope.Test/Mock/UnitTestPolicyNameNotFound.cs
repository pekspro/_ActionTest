using Pekspro.PolicyScope.LogicTest;
using Pekspro.PolicyScope.LogicTest.Workers;
using Pekspro.PolicyScope.Mock;
using System.Threading.Tasks;
using Xunit;

namespace Pekspro.PolicyScope.Test.Mock
{
    public class UnitTestPolicyNameNotFound
    {
        [Fact]
        public async Task TestNoAsyncPolicyWithNameFound()
        {
            // Arrange
            var policyScopeMock = PolicyScopeMock
                            .AsyncPolicy(PolicyNames.SyncPolicyName)
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
        public void TestNoSyncPolicyWithNameFound()
        {
            // Arrange
            var policyScopeMock = PolicyScopeMock
                            .SyncPolicy(PolicyNames.AsyncPolicyName)
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
