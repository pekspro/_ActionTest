﻿// This file has been autogenerated via
// Pekspro.PolicyScope.CodeGenerator.Mock.PolicyScopeServiceSelectorMockBuilderGenerator

namespace Pekspro.PolicyScope.Mock
{
    public class AsyncPolicyScopeServiceSelectorMockBuilder
    {
        public AsyncPolicyScopeServiceSelectorMockBuilder(PolicyScopeMockBuilder policyScopeMockBuilder)
        {
            PolicyScopeMockBuilder = policyScopeMockBuilder;
        }

        protected PolicyScopeMockBuilder PolicyScopeMockBuilder { get; }


        public AsyncNoServicePolicyScopeResultSelectorMockBuilder WithNoService()
        {
            return new AsyncNoServicePolicyScopeResultSelectorMockBuilder(PolicyScopeMockBuilder);
        }

        public AsyncServicePolicyScopeResultSelectorMockBuilder<T> WithService<T>(T service)
        {
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service!);

            return new AsyncServicePolicyScopeResultSelectorMockBuilder<T>(PolicyScopeMockBuilder);
        }

        public AsyncServicePolicyScopeResultSelectorMockBuilder<T1, T2> WithServices<T1, T2>(T1 service1, T2 service2)
        {
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T1));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service1!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T2));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service2!);

            return new AsyncServicePolicyScopeResultSelectorMockBuilder<T1, T2>(PolicyScopeMockBuilder);
        }

        public AsyncServicePolicyScopeResultSelectorMockBuilder<T1, T2, T3> WithServices<T1, T2, T3>(T1 service1, T2 service2, T3 service3)
        {
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T1));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service1!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T2));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service2!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T3));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service3!);

            return new AsyncServicePolicyScopeResultSelectorMockBuilder<T1, T2, T3>(PolicyScopeMockBuilder);
        }

        public AsyncServicePolicyScopeResultSelectorMockBuilder<T1, T2, T3, T4> WithServices<T1, T2, T3, T4>(T1 service1, T2 service2, T3 service3, T4 service4)
        {
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T1));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service1!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T2));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service2!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T3));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service3!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T4));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service4!);

            return new AsyncServicePolicyScopeResultSelectorMockBuilder<T1, T2, T3, T4>(PolicyScopeMockBuilder);
        }
    }

    public class SyncPolicyScopeServiceSelectorMockBuilder
    {
        public SyncPolicyScopeServiceSelectorMockBuilder(PolicyScopeMockBuilder policyScopeMockBuilder)
        {
            PolicyScopeMockBuilder = policyScopeMockBuilder;
        }

        protected PolicyScopeMockBuilder PolicyScopeMockBuilder { get; }


        public SyncNoServicePolicyScopeResultSelectorMockBuilder WithNoService()
        {
            return new SyncNoServicePolicyScopeResultSelectorMockBuilder(PolicyScopeMockBuilder);
        }

        public SyncServicePolicyScopeResultSelectorMockBuilder<T> WithService<T>(T service)
        {
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service!);

            return new SyncServicePolicyScopeResultSelectorMockBuilder<T>(PolicyScopeMockBuilder);
        }

        public SyncServicePolicyScopeResultSelectorMockBuilder<T1, T2> WithServices<T1, T2>(T1 service1, T2 service2)
        {
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T1));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service1!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T2));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service2!);

            return new SyncServicePolicyScopeResultSelectorMockBuilder<T1, T2>(PolicyScopeMockBuilder);
        }

        public SyncServicePolicyScopeResultSelectorMockBuilder<T1, T2, T3> WithServices<T1, T2, T3>(T1 service1, T2 service2, T3 service3)
        {
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T1));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service1!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T2));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service2!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T3));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service3!);

            return new SyncServicePolicyScopeResultSelectorMockBuilder<T1, T2, T3>(PolicyScopeMockBuilder);
        }

        public SyncServicePolicyScopeResultSelectorMockBuilder<T1, T2, T3, T4> WithServices<T1, T2, T3, T4>(T1 service1, T2 service2, T3 service3, T4 service4)
        {
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T1));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service1!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T2));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service2!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T3));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service3!);
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof(T4));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service4!);

            return new SyncServicePolicyScopeResultSelectorMockBuilder<T1, T2, T3, T4>(PolicyScopeMockBuilder);
        }
    }

}
