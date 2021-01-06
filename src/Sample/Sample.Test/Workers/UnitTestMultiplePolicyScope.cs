using Moq;
using Pekspro.PolicyScope.Mock;
using Sample.Services;
using Sample.Workers;
using System.Threading.Tasks;
using Xunit;

namespace Sample.Test.Workers
{
    public class UnitTestMultiplePolicyScope
    {
        [Fact]
        public async Task TestUseMultipleNestedScopesAsync()
        {
            // Arrange
            MyDatabaseContext myDatabaseContext = new MyDatabaseContext();
            MyDatabaseContext myDatabaseContext2 = new MyDatabaseContext();
            Mock<IDatabaseUpdater> databaseUpdater = new Mock<IDatabaseUpdater>();
            Mock<IDatabaseRemover> databaseRemover = new Mock<IDatabaseRemover>();

            databaseUpdater
                .Setup(d => d.UpdateAsync(myDatabaseContext))
                .Returns(Task.FromResult(42));

            PolicyScopeMock policyScopeMock = PolicyScopeMock
                            .AsyncPolicy(PolicyNames.Primary)
                            .WithServices(myDatabaseContext, databaseUpdater.Object)
                            .WithResultType<int>()
                            .AsyncPolicy(PolicyNames.Secondary)
                            .WithServices(myDatabaseContext2, databaseRemover.Object)
                            .WithNoResult()
                            .Build();

            MultiplePolicyScope logic = new MultiplePolicyScope(policyScopeMock.Object);

            // Act
            int result = await logic.UseMultipleNestedScopesAsync();

            // Assert
            policyScopeMock.VerifyRun(2);
            policyScopeMock
                .WherePolicyNameIs(PolicyNames.Primary)
                .VerifyRunOnce();
            policyScopeMock
                .WherePolicyNameIs(PolicyNames.Secondary)
                .VerifyRunOnce();

            Assert.Equal(42, result);

            databaseUpdater
                .Verify(d => d.UpdateAsync(myDatabaseContext), Times.Once());

            databaseRemover
                .Verify(d => d.RemoveAsync(myDatabaseContext2), Times.Once());
        }

        [Fact]
        public async Task TestUseMultipleSerialScopesAsync()
        {
            // Arrange
            MyDatabaseContext myDatabaseContext = new MyDatabaseContext();
            MyDatabaseContext myDatabaseContext2 = new MyDatabaseContext();
            Mock<IDatabaseUpdater> databaseUpdater = new Mock<IDatabaseUpdater>();
            Mock<IDatabaseRemover> databaseRemover = new Mock<IDatabaseRemover>();

            databaseUpdater
                .Setup(d => d.UpdateAsync(myDatabaseContext))
                .Returns(Task.FromResult(42));

            PolicyScopeMock policyScopeMock = PolicyScopeMock
                            .AsyncPolicy(PolicyNames.Primary)
                            .WithServices(myDatabaseContext, databaseUpdater.Object)
                            .WithResultType<int>()
                            .AsyncPolicy(PolicyNames.Secondary)
                            .WithServices(myDatabaseContext2, databaseRemover.Object)
                            .WithNoResult()
                            .Build();

            MultiplePolicyScope logic = new MultiplePolicyScope(policyScopeMock.Object);

            // Act
            int result = await logic.UseMultipleSerialScopesAsync();

            // Assert
            policyScopeMock.VerifyRun(2);
            Assert.Equal(42, result);

            databaseUpdater
                .Verify(d => d.UpdateAsync(myDatabaseContext), Times.Once());

            databaseRemover
                .Verify(d => d.RemoveAsync(myDatabaseContext2), Times.Once());
        }
    }
}
