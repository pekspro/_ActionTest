using Moq;
using Pekspro.PolicyScope.LogicTest.Services;
using Pekspro.PolicyScope.Mock;
using System.Threading.Tasks;
using Xunit;

namespace Pekspro.PolicyScope.Test.Mock
{
    public class UnitTestFilters
    {
        [Fact]
        public async Task TestWherePolicyIsAsync()
        {
            // Arrange
            Mock<IDummyService1> service1Mock = new Mock<IDummyService1>();
            Mock<IDummyService2> service2Mock = new Mock<IDummyService2>();

            PolicyScopeMock policyScopeMock = PolicyScopeMock
                            .AsyncPolicy("Primary")
                            .WithService(service1Mock.Object)
                            .WithResultType<int>()
                            .AsyncPolicy("Secondary")
                            .WithServices(service1Mock.Object, service2Mock.Object)
                            .WithNoResult()
                            .Build();

            MultipleUsage logic = new MultipleUsage(policyScopeMock.Object);

            // Act
            await logic.RunAsync();

            // Assert
            policyScopeMock
                .WherePolicyIsAsync()
                .VerifyRun(3);
            policyScopeMock
                .WherePolicyIsSync()
                .VerifyRunNever();
        }

        [Fact]
        public void TestWherePolicyIsSync()
        {
            // Arrange
            Mock<IDummyService1> service1Mock = new Mock<IDummyService1>();
            Mock<IDummyService2> service2Mock = new Mock<IDummyService2>();

            PolicyScopeMock policyScopeMock = PolicyScopeMock
                            .SyncPolicy("Primary")
                            .WithService(service1Mock.Object)
                            .WithResultType<int>()
                            .SyncPolicy("Secondary")
                            .WithServices(service1Mock.Object, service2Mock.Object)
                            .WithNoResult()
                            .Build();

            MultipleUsage logic = new MultipleUsage(policyScopeMock.Object);

            // Act
            logic.Run();

            // Assert
            policyScopeMock
                .WherePolicyIsSync()
                .VerifyRun(3);
            policyScopeMock
                .WherePolicyIsAsync()
                .VerifyRunNever();
        }

        [Fact]
        public async Task TestWherePolicyNameIs()
        {
            // Arrange
            Mock<IDummyService1> service1Mock = new Mock<IDummyService1>();
            Mock<IDummyService2> service2Mock = new Mock<IDummyService2>();

            PolicyScopeMock policyScopeMock = PolicyScopeMock
                            .AsyncPolicy("Primary")
                            .WithService(service1Mock.Object)
                            .WithResultType<int>()
                            .AsyncPolicy("Secondary")
                            .WithServices(service1Mock.Object, service2Mock.Object)
                            .WithNoResult()
                            .Build();

            MultipleUsage logic = new MultipleUsage(policyScopeMock.Object);

            // Act
            await logic.RunAsync();

            // Assert
            policyScopeMock
                .WherePolicyNameIs("Primary")
                .VerifyRunOnce();
            policyScopeMock
                .WherePolicyNameIs("Secondary")
                .VerifyRun(2);
            policyScopeMock
                .WherePolicyNameIs("Tertiary")
                .VerifyRunNever();
        }

        [Fact]
        public async Task TestWherePolicyNameIsNot()
        {
            // Arrange
            Mock<IDummyService1> service1Mock = new Mock<IDummyService1>();
            Mock<IDummyService2> service2Mock = new Mock<IDummyService2>();

            PolicyScopeMock policyScopeMock = PolicyScopeMock
                            .AsyncPolicy("Primary")
                            .WithService(service1Mock.Object)
                            .WithResultType<int>()
                            .AsyncPolicy("Secondary")
                            .WithServices(service1Mock.Object, service2Mock.Object)
                            .WithNoResult()
                            .Build();

            MultipleUsage logic = new MultipleUsage(policyScopeMock.Object);

            // Act
            await logic.RunAsync();

            // Assert
            policyScopeMock
                .WherePolicyNameIsNot("Primary")
                .VerifyRun(2);
            policyScopeMock
                .WherePolicyNameIsNot("Secondary")
                .VerifyRunOnce();
            policyScopeMock
                .WherePolicyNameIsNot("Tertiary")
                .VerifyRun(3);
        }


        public class MultipleUsage
        {
            internal IPolicyScopeBuilder PolicyScopeBuilder { get; }

            public MultipleUsage(IPolicyScopeBuilder policyScopeBuilder)
            {
                PolicyScopeBuilder = policyScopeBuilder;
            }

            public async Task<int> RunAsync()
            {
                for (int i = 0; i < 2; i++)
                {
                    await PolicyScopeBuilder
                        .WithAsyncPolicy("Secondary")
                        .WithServices<IDummyService1, IDummyService2>()
                        .WithNoResult()
                        .RunAsync(async (service1, service2) =>
                        {
                            await service1.CalculateAsync();
                        });
                }

                return await PolicyScopeBuilder
                            .WithAsyncPolicy("Primary")
                            .WithService<IDummyService1>()
                            .WithResult<int>()
                            .RunAsync(async (service1) =>
                            {
                                return await service1.CalculateAsync();
                            });
            }

            public int Run()
            {
                for (int i = 0; i < 2; i++)
                {
                    PolicyScopeBuilder
                        .WithSyncPolicy("Secondary")
                        .WithServices<IDummyService1, IDummyService2>()
                        .WithNoResult()
                        .Run((service1, service2) =>
                        {
                            service1.Calculate();
                        });
                }

                return PolicyScopeBuilder
                        .WithSyncPolicy("Primary")
                        .WithService<IDummyService1>()
                        .WithResult<int>()
                        .Run((service1) =>
                        {
                            return service1.Calculate();
                        });
            }
        }
    }
}
