using Pekspro.PolicyScope.CodeGenerator.Common;
using Pekspro.PolicyScope.CodeGenerator.LogicTest.Workers;
using System.Text;

namespace Pekspro.PolicyScope.CodeGenerator.LogicTest
{
    public class WorkerGenerator : CodeGeneratorBase
    {
        public WorkerGenerator()
            : base()
        {

        }

        public override string GetClassName(CodeNames codeNames)
        {
            return "Worker";
        }

        public override bool WriteClassFileContent(int maxServiceCount)
        {
            string fileContent = CreateClassFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope.LogicTest/Worker.cs", fileContent);
        }

        public string CreateClassFileContent(int maxServiceCount)
        {
            CodeNames codeNames = new CodeNames(maxServiceCount, false, false);
            LogicGenerator logicGenerator = new LogicGenerator();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pekspro.PolicyScope.LogicTest.Workers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pekspro.PolicyScope.LogicTest
{{
    public class Worker : BackgroundService
    {{
        public Worker(IServiceScopeFactory serviceScopeFactory, IHostApplicationLifetime hostApplicationLifetime)
        {{
            ServiceProviderFactory = serviceScopeFactory;
            HostApplicationLifetime = hostApplicationLifetime;
        }}

        public IServiceScopeFactory ServiceProviderFactory {{ get; }}

        public IHostApplicationLifetime HostApplicationLifetime {{ get; }}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {{
            await Task.Delay(100);
            Console.WriteLine();

            using (var scope = ServiceProviderFactory.CreateScope())
            {{
                {logicGenerator.GetInterfaceName(codeNames)} logic = scope.ServiceProvider.GetService<{logicGenerator.GetInterfaceName(codeNames)}>();");

            for (int asyncMode = 0; asyncMode < (GenerateSync ? 2 : 1); asyncMode++)
            {
                for (int resultMode = 0; resultMode < 2; resultMode++)
                {
                    for (int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
                    {
                        sb.AppendLine(CreateTestStep(new CodeNames(serviceCount, resultMode != 0, asyncMode == 0)));
                    }
                }
            }

            sb.Remove(sb.Length - 2, 2);

            sb.AppendLine(
@"            }

            Console.WriteLine();

            HostApplicationLifetime.StopApplication();
        }
    }
}");

            return sb.ToString();
        }

        public string CreateTestStep(CodeNames codeNames)
        {
            StringBuilder sb = new StringBuilder();

            LogicGenerator dummyLogicGenerator = new LogicGenerator();

            sb.AppendLine(
$@"                Console.WriteLine($""Running {{nameof(logic.{dummyLogicGenerator.CreateFunctionName(codeNames)})}}..."");
                {(codeNames.IsAsync ? "await " : "")}logic.{dummyLogicGenerator.CreateFunctionName(codeNames)}();");

            return sb.ToString();
        }
    }
}
