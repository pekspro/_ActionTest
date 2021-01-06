using Pekspro.PolicyScope.CodeGenerator.Common;
using Pekspro.PolicyScope.CodeGenerator.MainLibrary;
using System.Text;

namespace Pekspro.PolicyScope.CodeGenerator.Mock
{
    public class PolicyScopeServiceSelectorMockGenerator : CodeGeneratorBase
    {
        public PolicyScopeServiceSelectorMockGenerator()
            : base()
        {

        }

        public override string GetClassName(CodeNames codeNames)
        {
            PolicyScopeServiceSelectorGenerator policyScopeServiceSelectorGenerator =
                new PolicyScopeServiceSelectorGenerator();

            return policyScopeServiceSelectorGenerator.GetClassName(codeNames) + "Mock";
        }

        public override void WriteClassFileContent(int maxServiceCount)
        {
            string fileContent = CreateClassFileContent(maxServiceCount);

            WriteFileContent("Pekspro.PolicyScope.Mock/PolicyScopeServiceSelectorMock.cs", fileContent);
        }

        public string CreateClassFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"using System;
using System.Collections.Generic;
using System.Linq;

namespace Pekspro.PolicyScope.Mock
{");

            for (int asyncMode = 0; asyncMode < (GenerateSync ? 2 : 1); asyncMode++)
            {
                sb.AppendLine(CreateClassCode(new CodeNames(0, false, asyncMode == 0), maxServiceCount));
            }

            sb.Remove(sb.Length - 2, 2);

            sb.AppendLine(@"}");

            return sb.ToString();
        }

        public string CreateClassCode(CodeNames codeNameBase, int maxServiceCount)
        {
            PolicyScopeResultSelectorMockGenerator policyScopeResultSelectorMockGenerator =
                new PolicyScopeResultSelectorMockGenerator();
            PolicyScopeServiceSelectorGenerator policyScopeServiceSelectorGenerator =
                new PolicyScopeServiceSelectorGenerator();
            PolicyScopeResultSelectorGenerator servicePolicyScopeBuilderGenerator =
                new PolicyScopeResultSelectorGenerator();

            StringBuilder sb = new StringBuilder();

            sb.Append(
$@"    internal class {GetClassNameAndTemplateParamaters(codeNameBase)} : {policyScopeServiceSelectorGenerator.GetInterfaceNameAndTemplateParamaters(codeNameBase)}
    {{
        internal {GetClassName(codeNameBase)}(IEnumerable<PolicyScopeMockConfiguration> configurations)
        {{
             Configurations = configurations;
        }}

        internal IEnumerable<PolicyScopeMockConfiguration> Configurations {{ get; }}

");

            for (int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
            {
                if (serviceCount != 0)
                {
                    sb.AppendLine();
                }
                CodeNames codeNames = new CodeNames(serviceCount, false, codeNameBase.IsAsync);

                sb.Append(
$@"        public {servicePolicyScopeBuilderGenerator.GetInterfaceNameAndTemplateParamaters(codeNames)} {codeNames.FunctionWithServicesName}{codeNames.TemplateParameters}()
        {{
            var configurations = Configurations
                                    .Where(conf => conf.ServiceTypes.Count() == {codeNames.ServiceCount})");

                for (int i = 0; i < codeNames.ServiceCount; i++)
                {
                    sb.Append(
$@"
                                    .Where(conf => typeof({codeNames.ServicesTypes[i]}).IsAssignableFrom(conf.ServiceTypes[{i}]))"
                        );
                }


                sb.AppendLine(
$@";

            if(!configurations.Any())
            {{
                string errorMessage = ""Found no configuration with {codeNames.ServiceCount} {(codeNames.ServiceCount == 1 ? "service" : "services")}"";");

                for(int i = 0; i < codeNames.ServiceCount; i++)
                {
                    sb.AppendLine(
$@"                errorMessage += Environment.NewLine + "" and service {i} is "" + typeof({codeNames.ServicesTypes[i]});"
                        );
                }

                sb.AppendLine(
$@"                errorMessage += ""."";

                throw new PolicyScopeMockException(errorMessage, Configurations);
            }}

            return new {policyScopeResultSelectorMockGenerator.GetClassNameAndTemplateParamaters(codeNames)}(configurations);
        }}");
            }

            sb.AppendLine(
$@"    }}");

            return sb.ToString();
        }
    }
}
