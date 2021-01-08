using Pekspro.PolicyScope.CodeGenerator.Common;
using Pekspro.PolicyScope.CodeGenerator.MainLibrary;
using System.Linq;
using System.Text;

namespace Pekspro.PolicyScope.CodeGenerator.Mock
{
    public class PolicyScopeRunnerMockGenerator : CodeGeneratorBase
    {
        public PolicyScopeRunnerMockGenerator()
            : base()
        {

        }

        public override string GetClassName(CodeNames codeNames)
        {
            PolicyScopeRunnerGenerator policyScopeRunnerGenerator =
                new PolicyScopeRunnerGenerator();

            return policyScopeRunnerGenerator.GetClassName(codeNames) + "Mock";
        }

        public override bool WriteClassFileContent(int maxServiceCount)
        {
            string fileContent = CreateClassFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope.Mock/PolicyScopeRunnerMock.cs", fileContent);
        }

        public string CreateClassFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"using System;
using System.Threading.Tasks;

namespace Pekspro.PolicyScope.Mock
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

            sb.Remove(sb.Length - 2, 2);

            sb.AppendLine(@"}");

            return sb.ToString();
        }

        public string CreateClassCode(CodeNames codeNames)
        {
            PolicyScopeRunnerGenerator policyScopeRunnerGenerator =
                new PolicyScopeRunnerGenerator();

            StringBuilder sb = new StringBuilder();

            sb.Append(
$@"    internal class {GetClassNameAndTemplateParamaters(codeNames)} : {policyScopeRunnerGenerator.GetInterfaceNameAndTemplateParamaters(codeNames)}
    {{
        internal {GetClassName(codeNames)}(PolicyScopeMockConfiguration policyScopeMockConfiguration)
        {{
            PolicyScopeMockConfiguration = policyScopeMockConfiguration;
        }}

        internal PolicyScopeMockConfiguration PolicyScopeMockConfiguration {{ get; }}

");
            string retValueGrab = string.Empty;

            if(codeNames.HasReturnType)
            {
                retValueGrab = CodeNames.TResultName + " retValue = ";
            }

            if(codeNames.IsAsync)
            {
                sb.AppendLine(
$@"        public async Task{(codeNames.HasReturnType ? $"<{CodeNames.TResultName}>" : "")} {PolicyScopeRunnerGenerator.GetRunName(true)}({codeNames.GetFunctionDeclaration()} func)
        {{");
            }
            else
            {
                sb.AppendLine(
$@"        public {(codeNames.HasReturnType ? $"{CodeNames.TResultName}" : "void")} {PolicyScopeRunnerGenerator.GetRunName(false)}({codeNames.GetFunctionDeclaration()} func)
        {{");
            }

            if(codeNames.HasServices)
            {
                for(int i = 0; i < codeNames.ServiceCount; i++)
                {
                    sb.AppendLine(
$@"            var {codeNames.ServicesNames[i]} = ({codeNames.ServicesTypes[i]}) PolicyScopeMockConfiguration.ServiceInstances[{i}];");
                }

                sb.AppendLine();
            }

            if (codeNames.IsAsync)
            {
                sb.AppendLine(
$@"            {retValueGrab}await func.Invoke({string.Join(", ", codeNames.ServicesNames)}).ConfigureAwait(false);");
            }
            else
            {
                sb.AppendLine(
$@"            {retValueGrab} func.Invoke({string.Join(", ", codeNames.ServicesNames)});");
            }

            sb.AppendLine(
$@"
            PolicyScopeMockConfiguration.ExecutionCount++;");

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


    }
}
