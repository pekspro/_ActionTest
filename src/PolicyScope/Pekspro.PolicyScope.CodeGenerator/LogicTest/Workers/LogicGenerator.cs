using Pekspro.PolicyScope.CodeGenerator.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pekspro.PolicyScope.CodeGenerator.LogicTest.Workers
{
    public class LogicGenerator : CodeGeneratorBase
    {
        public LogicGenerator()
            : base()
        {

        }

        public override string GetClassName(CodeNames codeNames)
        {
            string name = "Logic";

            return name;
        }

        public override bool WriteClassFileContent(int maxServiceCount)
        {
            string fileContent = CreateClassFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope.LogicTest/Workers/Logic.cs", fileContent);
        }

        public string CreateClassFileContent(int maxServiceCount)
        {
            var codeNames = new CodeNames(maxServiceCount, false, false);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"using Pekspro.PolicyScope.LogicTest.Services;
using System.Threading.Tasks;

namespace Pekspro.PolicyScope.LogicTest.Workers
{{
    public class {GetClassName(codeNames)} : {GetInterfaceName(codeNames)}
    {{
        public IPolicyScopeBuilder PolicyScopeBuilder {{ get; }}

        public {GetClassName(codeNames)}(IPolicyScopeBuilder policyScopeBuilder)
        {{
            PolicyScopeBuilder = policyScopeBuilder;
        }}

        public int LatestResult {{ get; set; }}
");

            for (int asyncMode = 0; asyncMode < (GenerateSync ? 2 : 1); asyncMode++)
            {
                for (int resultMode = 0; resultMode < 2; resultMode++)
                {
                    for (int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
                    {
                        sb.AppendLine(CreateFunctionCode(new CodeNames(serviceCount, resultMode != 0, asyncMode == 0)));
                    }
                }
            }

            sb.AppendLine(@"    }
}");

            return sb.ToString();
        }

        public string CreateFunctionName(CodeNames codeNames)
        {
            string name = "Run";

            if (codeNames.HasReturnType)
            {
                name += "WithResult";
            }
            else
            {
                name += "WithNoResult";
            }

            if (codeNames.HasServices)
            {
                name += $"{codeNames.ServicesTypes.Count()}Service";
            }
            else
            {
                name += "NoService";
            }

            if (codeNames.IsAsync)
            {
                name += "Async";
            }

            return name;
        }

        public string CreateFunctionDeclaration(CodeNames codeNames)
        {
            StringBuilder sb = new StringBuilder();

            if (codeNames.IsAsync)
            {
                if (codeNames.HasReturnType)
                {
                    sb.Append("Task<int>");
                }
                else
                {
                    sb.Append("Task");
                }
            }
            else
            {
                if (codeNames.HasReturnType)
                {
                    sb.Append("int");
                }
                else
                {
                    sb.Append("void");
                }
            }

            sb.Append($" {CreateFunctionName(codeNames)}()");

            return sb.ToString();
        }

        public string CreateFunctionCode(CodeNames codeNames)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($@"        public {CreateFunctionDeclaration(codeNames)}");

            sb.Append(
$@"        {{
            LatestResult = -1;

            ");

            if (codeNames.IsAsync || codeNames.HasReturnType)
            {
                sb.Append("return ");
            }

            sb.AppendLine("PolicyScopeBuilder");

            if (codeNames.IsAsync)
            {
                sb.AppendLine("                .WithAsyncPolicy(PolicyNames.AsyncPolicyName)");
            }
            else
            {
                sb.AppendLine("                .WithSyncPolicy(PolicyNames.SyncPolicyName)");
            }

            if (!codeNames.HasServices)
            {
                sb.AppendLine($"                .{codeNames.FunctionWithServicesName}()");
            }
            else
            {
                sb.Append($"                .{codeNames.FunctionWithServicesName}<");

                var serviceNames = Enumerable
                    .Range(1, codeNames.ServiceCount)
                    .Select(r => "IDummyService" + r);

                sb.Append(string.Join(", ", serviceNames));
                sb.AppendLine(">()");
            }

            if (!codeNames.HasReturnType)
            {
                sb.AppendLine("                .WithNoResult()");
            }
            else
            {
                sb.AppendLine("                .WithResult<int>()");
            }

            sb.Append($"                .{(codeNames.IsAsync ? "RunAsync" : "Run")}(");
            if (codeNames.IsAsync && codeNames.HasServices)
            {
                sb.Append("async ");
            }

            if (!codeNames.HasServices)
            {
                sb.Append("()");
            }
            else
            {
                var serviceNames = Enumerable
                    .Range(1, codeNames.ServiceCount)
                    .Select(r => "service" + r);

                sb.Append("(");
                sb.Append(string.Join(", ", serviceNames));
                sb.Append(")");
            }

            sb.AppendLine(" =>");
            sb.AppendLine(
@"                {
                    LatestResult = 0;");

            if (codeNames.HasServices)
            {
                var serviceNames = Enumerable
                    .Range(1, codeNames.ServiceCount)
                    .Select(r => "service" + r);

                foreach (var serviceName in serviceNames)
                {
                    if (codeNames.IsAsync)
                    {
                        sb.AppendLine(
@$"                    LatestResult += await {serviceName}.CalculateAsync();");
                    }
                    else
                    {
                        sb.AppendLine(
@$"                    LatestResult += {serviceName}.Calculate();");
                    }
                }
            }

            if (codeNames.IsAsync)
            {
                if (codeNames.HasReturnType)
                {
                    sb.AppendLine();
                    if (!codeNames.HasServices)
                    {
                        sb.AppendLine(
    @"                    return Task.FromResult(LatestResult);");
                    }
                    else
                    {
                        sb.AppendLine(
@"                    return LatestResult;");
                    }
                }
                else if (!codeNames.HasServices)
                {
                    sb.AppendLine();
                    sb.AppendLine(
@"                    return Task.CompletedTask;");
                }
            }
            else
            {
                if (codeNames.HasReturnType)
                {
                    sb.AppendLine();

                    sb.AppendLine(
@"                    return LatestResult;");
                }
            }

            sb.AppendLine(
@"                });
        }");

            return sb.ToString();
        }


        public override bool WriteInterfaceFileContent(int maxServiceCount)
        {
            string fileContent = CreateInterfaceFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope.LogicTest/Workers/ILogic.cs", fileContent);
        }

        public string CreateInterfaceFileContent(int maxServiceCount)
        {
            CodeNames codeNames = new CodeNames(maxServiceCount, false, false);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@$"using System.Threading.Tasks;

namespace Pekspro.PolicyScope.LogicTest.Workers
{{
    public interface {GetInterfaceName(codeNames)}
    {{");

            for (int asyncMode = 0; asyncMode < (GenerateSync ? 2 : 1); asyncMode++)
            {
                for (int resultMode = 0; resultMode < 2; resultMode++)
                {
                    for (int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
                    {
                        sb.AppendLine(CreateInterfaceFunctionCode(new CodeNames(serviceCount, resultMode != 0, asyncMode == 0)));
                    }
                }
            }

            sb.AppendLine(@"    }");
            sb.AppendLine(@"}");

            return sb.ToString();
        }

        public string CreateInterfaceFunctionCode(CodeNames codeNames)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($@"        public {CreateFunctionDeclaration(codeNames)};");

            return sb.ToString();
        }
    }
}
