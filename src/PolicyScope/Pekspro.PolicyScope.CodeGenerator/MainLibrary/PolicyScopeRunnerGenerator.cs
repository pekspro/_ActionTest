using Pekspro.PolicyScope.CodeGenerator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pekspro.PolicyScope.CodeGenerator.MainLibrary
{
    public class PolicyScopeRunnerGenerator : CodeGeneratorBase
    {
        public PolicyScopeRunnerGenerator()
            : base()
        {

        }

        public override string GetClassName(CodeNames codeNames)
        {
            string name = codeNames.ClassNamePrefix +
                            codeNames.ClassNameServicePrefix +
                            codeNames.ClassNameResultPrefix +
                            "PolicyScopeRunner";

            return name;
        }

        public override bool WriteClassFileContent(int maxServiceCount)
        {
            string fileContent = CreateClassFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope/PolicyScopeRunner.cs", fileContent);
        }

        public string CreateClassFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Pekspro.PolicyScope
{");

            for (int asyncMode = 0; asyncMode < (GenerateSync ? 2 : 1); asyncMode++)
            {
                for (int resultMode = 0; resultMode < 2; resultMode++)
                {
                    for (int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
                    {
                        sb.AppendLine(CreateClassCode(new CodeNames(serviceCount, resultMode != 0, asyncMode == 0)));
                    }
                }
            }

            sb.AppendLine(@"}");

            return sb.ToString();
        }

        public static string GetRunName(bool isAsync)
        {
            if(isAsync)
            {
                return "RunAsync";
            }
            else
            {
                return "Run";
            }
        }

        public string CreateClassCode(CodeNames codeNames)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(
$@"    internal class {GetClassNameAndTemplateParamaters(codeNames)} : {codeNames.BaseClassName}, {GetInterfaceNameAndTemplateParamaters(codeNames)}
    {{
        internal {GetClassName(codeNames)}({codeNames.BaseClassName} settings)
            : base(settings)
        {{
        }}

");
            string retValueDefinition = string.Empty;
            string retValueGrab = string.Empty;

            if(codeNames.HasReturnType)
            {
                retValueDefinition =
@"
            TResult retValue = default!;
";
                retValueGrab = "retValue = ";
            }

            if(codeNames.IsAsync)
            {
                sb.AppendLine(
$@"        public async Task{(codeNames.HasReturnType ? $"<{CodeNames.TResultName}>" : "")} {GetRunName(true)}({codeNames.GetFunctionDeclaration()} func)
        {{{retValueDefinition}
            await Policy.ExecuteAsync(async () =>
            {{");
            }
            else
            {
                sb.AppendLine(
$@"        public {(codeNames.HasReturnType ? $"{CodeNames.TResultName}" : "void")} {GetRunName(false)}({codeNames.GetFunctionDeclaration()} func)
        {{{retValueDefinition}
            Policy.Execute(() =>
            {{");
            }

            if(codeNames.HasServices)
            {
                sb.AppendLine(
$@"                using (var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {{");

                for(int i = 0; i < codeNames.ServiceCount; i++)
                {
                    sb.AppendLine(
$@"                    var {codeNames.ServicesNames[i]} = scope.ServiceProvider.GetService<{codeNames.ServicesTypes[i]}>()!;");

                }

                sb.AppendLine();
                sb.Append("    ");
            }

            if (codeNames.IsAsync)
            {
                sb.AppendLine(
$@"                {retValueGrab}await func.Invoke({string.Join(", ", codeNames.ServicesNames)}).ConfigureAwait(false);");
            }
            else
            {
                sb.AppendLine(
$@"                {retValueGrab} func.Invoke({string.Join(", ", codeNames.ServicesNames)});");
            }

            if (codeNames.HasServices)
            {
                sb.AppendLine(
$@"                }}");
            }

            sb.AppendLine(
$@"            }});");

            if (codeNames.HasReturnType)
            {
                sb.AppendLine(
$@"
            return retValue;");
            }

            sb.AppendLine(
$@"        }}
    }}");

            return sb.ToString();
        }


        public override bool WriteInterfaceFileContent(int maxServiceCount)
        {
            string fileContent = CreateInterfaceFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope/IPolicyScopeRunner.cs", fileContent);
        }

        public string CreateInterfaceFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"using System;
using System.Threading.Tasks;

namespace Pekspro.PolicyScope
{");

            for(int asyncMode = 0; asyncMode < (GenerateSync ? 2 : 1); asyncMode++)
            {
                for(int resultMode = 0; resultMode < 2; resultMode++)
                {
                    for(int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
                    {
                        sb.AppendLine(CreateInterfaceCode(new CodeNames(serviceCount, resultMode != 0, asyncMode == 0)));
                    }
                }
            }

            sb.AppendLine(@"}");

            return sb.ToString();
        }

        public string CreateInterfaceCode(CodeNames codeNames)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(
$@"    public interface {GetInterfaceNameAndTemplateParamaters(codeNames)}
    {{
");

            if (codeNames.IsAsync)
            {
                sb.AppendLine(
$@"        Task{(codeNames.HasReturnType ? $"<{CodeNames.TResultName}>" : "")} RunAsync({codeNames.GetFunctionDeclaration()} func);");
            }
            else
            {
                sb.AppendLine(
$@"        {(codeNames.HasReturnType ? $"{CodeNames.TResultName}" : "void")} Run({codeNames.GetFunctionDeclaration()} func);");
            }

            sb.AppendLine(
$@"    }}");

            return sb.ToString();
        }
    }
}
