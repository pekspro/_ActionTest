using System;
using System.Collections.Generic;
using System.Linq;

namespace Pekspro.PolicyScope.Mock
{
    internal class PolicyScopeMockConfiguration
    {
        public bool IsAsync { get; internal set; }

        public string PolicyName { get; internal set; } = string.Empty;

        public Type? ReturnType { get; internal set; }

        public List<Type> ServiceTypes { get; internal set; } = new List<Type>();

        public List<object> ServiceInstances { get; internal set; } = new List<object>();

        public int ExecutionCount { get; internal set; }
    }
}
