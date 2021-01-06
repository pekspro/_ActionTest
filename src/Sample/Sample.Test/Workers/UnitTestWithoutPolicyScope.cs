using Microsoft.Extensions.DependencyInjection;
using Moq;
using Polly;
using Polly.Registry;
using Sample.Services;
using Sample.Workers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sample.Test.Workers
{
    public class UnitTestWithoutPolicyScope
    {
        [Fact]
        public async Task TestUpdateWithoutPolicyScopeAsync()
        {
            // Arrange
            MyDatabaseContext myDatabaseContext = new MyDatabaseContext();
            Mock<IDatabaseUpdater> databaseManipulator = new Mock<IDatabaseUpdater>();

            databaseManipulator
                .Setup(d => d.UpdateAsync(myDatabaseContext))
                .Returns(Task.FromResult(42));

            Mock<IServiceScopeFactory> serviceScopeFactory = new Mock<IServiceScopeFactory>();
            Mock<IServiceScope> serviceScope = new Mock<IServiceScope>();
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            Mock<IReadOnlyPolicyRegistry<string>> policyRegistry = new Mock<IReadOnlyPolicyRegistry<string>>();
            IAsyncPolicy policy = Policy.NoOpAsync();

            serviceScopeFactory
                .Setup(s => s.CreateScope())
                .Returns(serviceScope.Object);

            serviceScope
                .Setup(s => s.ServiceProvider)
                .Returns(serviceProvider.Object);

            serviceProvider
                .Setup(s => s.GetService(typeof(MyDatabaseContext)))
                .Returns(myDatabaseContext);

            serviceProvider
                .Setup(s => s.GetService(typeof(IDatabaseUpdater)))
                .Returns(databaseManipulator.Object);

            policyRegistry
                .Setup(p => p.Get<IAsyncPolicy>(PolicyNames.Primary))
                .Returns(policy);

            WithoutPolicyScope logic = new WithoutPolicyScope(serviceScopeFactory.Object, policyRegistry.Object);

            // Act
            int result = await logic.UpdateWithoutPolicyScopeAsync();

            // Assert
            Assert.Equal(42, result);
        }
    }
}
