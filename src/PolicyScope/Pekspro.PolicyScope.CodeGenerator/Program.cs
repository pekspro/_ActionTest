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
        static int Main(string[] args)
        {
            int maxServiceCount = 4;

            if(args.Length >= 1)
            {
                Common.CodeGeneratorBase.BaseDirectory = args[0];
            }

            int fileUpdateCount = 0;
            Console.WriteLine("Autogenerating code for main library...");

            PolicyScopeResultSelectorGenerator servicePolicyScopeBuilderGenerator
                = new PolicyScopeResultSelectorGenerator();
            fileUpdateCount += servicePolicyScopeBuilderGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeRunnerGenerator policyRunnerGenerator 
                = new PolicyScopeRunnerGenerator();
            fileUpdateCount += policyRunnerGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeServiceSelectorGenerator policyScopeServiceSelectorGenerator =
                new PolicyScopeServiceSelectorGenerator();
            fileUpdateCount += policyScopeServiceSelectorGenerator.WriteAllFiles(maxServiceCount);

            Console.WriteLine();
            Console.WriteLine("Autogenerating code for mock library...");

            PolicyScopeResultSelectorMockBuilderGenerator policyScopeResultSelectorMockBuilderGenerator = 
                new PolicyScopeResultSelectorMockBuilderGenerator();
            fileUpdateCount += policyScopeResultSelectorMockBuilderGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeResultSelectorMockGenerator policyScopeResultSelectorMockGenerator = 
                new PolicyScopeResultSelectorMockGenerator();
            fileUpdateCount += policyScopeResultSelectorMockGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeRunnerMockGenerator policyScopeRunnerMockGenerator 
                = new PolicyScopeRunnerMockGenerator();
            fileUpdateCount += policyScopeRunnerMockGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeServiceSelectorMockBuilderGenerator policyScopeServiceSelectorMockBuilderGenerator =
                new PolicyScopeServiceSelectorMockBuilderGenerator();
            fileUpdateCount += policyScopeServiceSelectorMockBuilderGenerator.WriteAllFiles(maxServiceCount);

            PolicyScopeServiceSelectorMockGenerator policyScopeServiceSelectorMockGenerator = 
                new PolicyScopeServiceSelectorMockGenerator();
            fileUpdateCount += policyScopeServiceSelectorMockGenerator.WriteAllFiles(maxServiceCount);

            Console.WriteLine(); 
            Console.WriteLine("Autogenerating code for logic test...");

            LogicGenerator logicGenerator =
                new LogicGenerator();
            fileUpdateCount += logicGenerator.WriteAllFiles(maxServiceCount);

            WorkerGenerator workerGenerator = 
                new WorkerGenerator();
            fileUpdateCount += workerGenerator.WriteAllFiles(maxServiceCount);

            Console.WriteLine(); 
            Console.WriteLine("Autogenerating code for test...");

            UnitTestLogicGenerator unitTestLogicGenerator =
                new UnitTestLogicGenerator();
            fileUpdateCount += unitTestLogicGenerator.WriteAllFiles(maxServiceCount);

            UnitTestResourceNotFoundGenerator unitTestResourceNotFoundGenerator =
                new UnitTestResourceNotFoundGenerator();
            fileUpdateCount += unitTestResourceNotFoundGenerator.WriteAllFiles(maxServiceCount);

            UnitTestServiceNotFoundGenerator unitTestServiceNotFoundGenerator = 
                new UnitTestServiceNotFoundGenerator();
            fileUpdateCount += unitTestServiceNotFoundGenerator.WriteAllFiles(maxServiceCount);

            Console.WriteLine(); 
            Console.WriteLine($"Autogenerating completed. {fileUpdateCount} files updated.");

            return fileUpdateCount;
        }
    }
}
