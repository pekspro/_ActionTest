﻿// This file has been autogenerated via
// Pekspro.PolicyScope.CodeGenerator.Mock.PolicyScopeServiceSelectorMockGenerator

using System;
using System.Collections.Generic;
using System.Linq;

namespace Pekspro.PolicyScope.Mock
{
    internal class AsyncPolicyScopeServiceSelectorMock : IAsyncPolicyScopeServiceSelector
    {
        internal AsyncPolicyScopeServiceSelectorMock(IEnumerable<PolicyScopeMockConfiguration> configurations)
        {
             Configurations = configurations;
        }

        internal IEnumerable<PolicyScopeMockConfiguration> Configurations { get; }

        public IAsyncPolicyScopeResultSelector WithNoService()
        {
            var configurations = Configurations
                                    .Where(conf => conf.ServiceTypes.Count() == 0);

            if(!configurations.Any())
            {
                string errorMessage = "Found no configuration with 0 services";
                errorMessage += ".";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new AsyncPolicyScopeResultSelectorMock(configurations);
        }

        public IAsyncPolicyScopeResultSelector<T> WithService<T>()
        {
            var configurations = Configurations
                                    .Where(conf => conf.ServiceTypes.Count() == 1)
                                    .Where(conf => typeof(T).IsAssignableFrom(conf.ServiceTypes[0]));

            if(!configurations.Any())
            {
                string errorMessage = "Found no configuration with 1 service";
                errorMessage += Environment.NewLine + " and service 0 is " + typeof(T);
                errorMessage += ".";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new AsyncPolicyScopeResultSelectorMock<T>(configurations);
        }

        public IAsyncPolicyScopeResultSelector<T1, T2> WithServices<T1, T2>()
        {
            var configurations = Configurations
                                    .Where(conf => conf.ServiceTypes.Count() == 2)
                                    .Where(conf => typeof(T1).IsAssignableFrom(conf.ServiceTypes[0]))
                                    .Where(conf => typeof(T2).IsAssignableFrom(conf.ServiceTypes[1]));

            if(!configurations.Any())
            {
                string errorMessage = "Found no configuration with 2 services";
                errorMessage += Environment.NewLine + " and service 0 is " + typeof(T1);
                errorMessage += Environment.NewLine + " and service 1 is " + typeof(T2);
                errorMessage += ".";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new AsyncPolicyScopeResultSelectorMock<T1, T2>(configurations);
        }

        public IAsyncPolicyScopeResultSelector<T1, T2, T3> WithServices<T1, T2, T3>()
        {
            var configurations = Configurations
                                    .Where(conf => conf.ServiceTypes.Count() == 3)
                                    .Where(conf => typeof(T1).IsAssignableFrom(conf.ServiceTypes[0]))
                                    .Where(conf => typeof(T2).IsAssignableFrom(conf.ServiceTypes[1]))
                                    .Where(conf => typeof(T3).IsAssignableFrom(conf.ServiceTypes[2]));

            if(!configurations.Any())
            {
                string errorMessage = "Found no configuration with 3 services";
                errorMessage += Environment.NewLine + " and service 0 is " + typeof(T1);
                errorMessage += Environment.NewLine + " and service 1 is " + typeof(T2);
                errorMessage += Environment.NewLine + " and service 2 is " + typeof(T3);
                errorMessage += ".";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new AsyncPolicyScopeResultSelectorMock<T1, T2, T3>(configurations);
        }

        public IAsyncPolicyScopeResultSelector<T1, T2, T3, T4> WithServices<T1, T2, T3, T4>()
        {
            var configurations = Configurations
                                    .Where(conf => conf.ServiceTypes.Count() == 4)
                                    .Where(conf => typeof(T1).IsAssignableFrom(conf.ServiceTypes[0]))
                                    .Where(conf => typeof(T2).IsAssignableFrom(conf.ServiceTypes[1]))
                                    .Where(conf => typeof(T3).IsAssignableFrom(conf.ServiceTypes[2]))
                                    .Where(conf => typeof(T4).IsAssignableFrom(conf.ServiceTypes[3]));

            if(!configurations.Any())
            {
                string errorMessage = "Found no configuration with 4 services";
                errorMessage += Environment.NewLine + " and service 0 is " + typeof(T1);
                errorMessage += Environment.NewLine + " and service 1 is " + typeof(T2);
                errorMessage += Environment.NewLine + " and service 2 is " + typeof(T3);
                errorMessage += Environment.NewLine + " and service 3 is " + typeof(T4);
                errorMessage += ".";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new AsyncPolicyScopeResultSelectorMock<T1, T2, T3, T4>(configurations);
        }
    }

    internal class SyncPolicyScopeServiceSelectorMock : ISyncPolicyScopeServiceSelector
    {
        internal SyncPolicyScopeServiceSelectorMock(IEnumerable<PolicyScopeMockConfiguration> configurations)
        {
             Configurations = configurations;
        }

        internal IEnumerable<PolicyScopeMockConfiguration> Configurations { get; }

        public ISyncPolicyScopeResultSelector WithNoService()
        {
            var configurations = Configurations
                                    .Where(conf => conf.ServiceTypes.Count() == 0);

            if(!configurations.Any())
            {
                string errorMessage = "Found no configuration with 0 services";
                errorMessage += ".";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new SyncPolicyScopeResultSelectorMock(configurations);
        }

        public ISyncPolicyScopeResultSelector<T> WithService<T>()
        {
            var configurations = Configurations
                                    .Where(conf => conf.ServiceTypes.Count() == 1)
                                    .Where(conf => typeof(T).IsAssignableFrom(conf.ServiceTypes[0]));

            if(!configurations.Any())
            {
                string errorMessage = "Found no configuration with 1 service";
                errorMessage += Environment.NewLine + " and service 0 is " + typeof(T);
                errorMessage += ".";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new SyncPolicyScopeResultSelectorMock<T>(configurations);
        }

        public ISyncPolicyScopeResultSelector<T1, T2> WithServices<T1, T2>()
        {
            var configurations = Configurations
                                    .Where(conf => conf.ServiceTypes.Count() == 2)
                                    .Where(conf => typeof(T1).IsAssignableFrom(conf.ServiceTypes[0]))
                                    .Where(conf => typeof(T2).IsAssignableFrom(conf.ServiceTypes[1]));

            if(!configurations.Any())
            {
                string errorMessage = "Found no configuration with 2 services";
                errorMessage += Environment.NewLine + " and service 0 is " + typeof(T1);
                errorMessage += Environment.NewLine + " and service 1 is " + typeof(T2);
                errorMessage += ".";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new SyncPolicyScopeResultSelectorMock<T1, T2>(configurations);
        }

        public ISyncPolicyScopeResultSelector<T1, T2, T3> WithServices<T1, T2, T3>()
        {
            var configurations = Configurations
                                    .Where(conf => conf.ServiceTypes.Count() == 3)
                                    .Where(conf => typeof(T1).IsAssignableFrom(conf.ServiceTypes[0]))
                                    .Where(conf => typeof(T2).IsAssignableFrom(conf.ServiceTypes[1]))
                                    .Where(conf => typeof(T3).IsAssignableFrom(conf.ServiceTypes[2]));

            if(!configurations.Any())
            {
                string errorMessage = "Found no configuration with 3 services";
                errorMessage += Environment.NewLine + " and service 0 is " + typeof(T1);
                errorMessage += Environment.NewLine + " and service 1 is " + typeof(T2);
                errorMessage += Environment.NewLine + " and service 2 is " + typeof(T3);
                errorMessage += ".";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new SyncPolicyScopeResultSelectorMock<T1, T2, T3>(configurations);
        }

        public ISyncPolicyScopeResultSelector<T1, T2, T3, T4> WithServices<T1, T2, T3, T4>()
        {
            var configurations = Configurations
                                    .Where(conf => conf.ServiceTypes.Count() == 4)
                                    .Where(conf => typeof(T1).IsAssignableFrom(conf.ServiceTypes[0]))
                                    .Where(conf => typeof(T2).IsAssignableFrom(conf.ServiceTypes[1]))
                                    .Where(conf => typeof(T3).IsAssignableFrom(conf.ServiceTypes[2]))
                                    .Where(conf => typeof(T4).IsAssignableFrom(conf.ServiceTypes[3]));

            if(!configurations.Any())
            {
                string errorMessage = "Found no configuration with 4 services";
                errorMessage += Environment.NewLine + " and service 0 is " + typeof(T1);
                errorMessage += Environment.NewLine + " and service 1 is " + typeof(T2);
                errorMessage += Environment.NewLine + " and service 2 is " + typeof(T3);
                errorMessage += Environment.NewLine + " and service 3 is " + typeof(T4);
                errorMessage += ".";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }

            return new SyncPolicyScopeResultSelectorMock<T1, T2, T3, T4>(configurations);
        }
    }
}
