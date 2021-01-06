using Pekspro.PolicyScope.CodeGenerator.LogicTest;
using Pekspro.PolicyScope.CodeGenerator.LogicTest.Workers;
using Pekspro.PolicyScope.CodeGenerator.MainLibrary;
using Pekspro.PolicyScope.CodeGenerator.Mock;
using Pekspro.PolicyScope.CodeGenerator.Test.LogicTest.Workers;
using Pekspro.PolicyScope.CodeGenerator.Test.Mock;
using System;

namespace Pekspro.PolicyScope.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxServiceCount = 4;

            Console.WriteLine("Autogenerating code for main library...");

            PolicyScopeResultSelectorGenerator servicePolicyScopeBuilderGenerator
                = new PolicyScopeResultSelectorGenerator();
            servicePolicyScopeBuilderGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeRunnerGenerator policyRunnerGenerator 
                = new PolicyScopeRunnerGenerator();
            policyRunnerGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeServiceSelectorGenerator policyScopeServiceSelectorGenerator =
                new PolicyScopeServiceSelectorGenerator();
            policyScopeServiceSelectorGenerator.WriteAllFiles(maxServiceCount);

            Console.WriteLine("Autogenerating code for mock library...");

            PolicyScopeResultSelectorMockBuilderGenerator policyScopeResultSelectorMockBuilderGenerator = 
                new PolicyScopeResultSelectorMockBuilderGenerator();
            policyScopeResultSelectorMockBuilderGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeResultSelectorMockGenerator policyScopeResultSelectorMockGenerator = 
                new PolicyScopeResultSelectorMockGenerator();
            policyScopeResultSelectorMockGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeRunnerMockGenerator policyScopeRunnerMockGenerator 
                = new PolicyScopeRunnerMockGenerator();
            policyScopeRunnerMockGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeServiceSelectorMockBuilderGenerator policyScopeServiceSelectorMockBuilderGenerator =
                new PolicyScopeServiceSelectorMockBuilderGenerator();
            policyScopeServiceSelectorMockBuilderGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeServiceSelectorMockGenerator policyScopeServiceSelectorMockGenerator = 
                new PolicyScopeServiceSelectorMockGenerator();
            policyScopeServiceSelectorMockGenerator.WriteAllFiles(maxServiceCount);

            Console.WriteLine("Autogenerating code for logic test...");

            LogicGenerator logicGenerator =
                new LogicGenerator();
            logicGenerator.WriteAllFiles(maxServiceCount);

            WorkerGenerator workerGenerator = 
                new WorkerGenerator();
            workerGenerator.WriteAllFiles(maxServiceCount);

            Console.WriteLine("Autogenerating code for test...");

            UnitTestLogicGenerator unitTestLogicGenerator =
                new UnitTestLogicGenerator();
            unitTestLogicGenerator.WriteAllFiles(maxServiceCount);

            UnitTestResourceNotFoundGenerator unitTestResourceNotFoundGenerator =
                new UnitTestResourceNotFoundGenerator();
            unitTestResourceNotFoundGenerator.WriteAllFiles(maxServiceCount);

            UnitTestServiceNotFoundGenerator unitTestServiceNotFoundGenerator = 
                new UnitTestServiceNotFoundGenerator();
            unitTestServiceNotFoundGenerator.WriteAllFiles(maxServiceCount);

            Console.WriteLine("Autogenerating completed.");
        }
    }
}
