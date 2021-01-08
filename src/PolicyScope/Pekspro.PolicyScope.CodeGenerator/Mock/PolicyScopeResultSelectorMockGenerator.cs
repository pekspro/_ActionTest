using Pekspro.PolicyScope.CodeGenerator.Common;
using Pekspro.PolicyScope.CodeGenerator.MainLibrary;
using System.Text;

namespace Pekspro.PolicyScope.CodeGenerator.Mock
{
    public class PolicyScopeResultSelectorMockGenerator : CodeGeneratorBase
    {
        public PolicyScopeResultSelectorMockGenerator()
            : base()
        {

        }

        public override string GetClassName(CodeNames codeNames)
        {
            PolicyScopeResultSelectorGenerator policyScopeResultSelectorGenerator =
                new PolicyScopeResultSelectorGenerator();

            return policyScopeResultSelectorGenerator.GetClassName(codeNames) + "Mock";
        }

        public override bool WriteClassFileContent(int maxServiceCount)
        {
            string fileContent = CreateClassFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope.Mock/PolicyScopeResultSelectorMock.cs", fileContent);
        }

        public string CreateClassFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"using System.Collections.Generic;
using System.Linq;

namespace Pekspro.PolicyScope.Mock
{");

            for (int asyncMode = 0; asyncMode < (GenerateSync ? 2 : 1); asyncMode++)
            {
                for (int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
                {
                    sb.AppendLine(CreateClassCode(new CodeNames(serviceCount, false, asyncMode == 0)));
                }
            }

            sb.Remove(sb.Length - 2, 2);

            sb.AppendLine(@"}");

            return sb.ToString();
        }

        public string CreateClassCode(CodeNames codeNames)
        {
            PolicyScopeResultSelectorGenerator policyScopeResultSelectorGenerator =
                new PolicyScopeResultSelectorGenerator();

            StringBuilder sb = new StringBuilder();

            sb.Append(
$@"    internal class {GetClassNameAndTemplateParamaters(codeNames)} : {policyScopeResultSelectorGenerator.GetInterfaceNameAndTemplateParamaters(codeNames)}
    {{
        internal {GetClassName(codeNames)}(IEnumerable<PolicyScopeMockConfiguration> configurations)
        {{
            Configurations = configurations;
        }}

        internal IEnumerable<PolicyScopeMockConfiguration> Configurations {{ get; }} 

");
            CodeNames codeNameWithResult = new CodeNames(codeNames.ServiceCount, true, codeNames.IsAsync);

            PolicyScopeRunnerGenerator policyRunnerGenerator = new PolicyScopeRunnerGenerator();
            PolicyScopeRunnerMockGenerator policyScopeRunnerMockGenerator = new PolicyScopeRunnerMockGenerator();

            sb.AppendLine(
$@"        public {policyRunnerGenerator.GetInterfaceNameAndTemplateParamaters(codeNames)} {PolicyScopeResultSelectorGenerator.WithNoResultName}()
        {{
            var config = Configurations.FirstOrDefault(conf => conf.ReturnType == null);

            if(config == null)
            {{
                throw new PolicyScopeMockException($""No matching policy scope mock configuration was found. Found no configuration with no result type."", Configurations);
            }}

            return new {policyScopeRunnerMockGenerator.GetClassNameAndTemplateParamaters(codeNames)}(config);
        }}

        public {policyRunnerGenerator.GetInterfaceNameAndTemplateParamaters(codeNameWithResult)} {PolicyScopeResultSelectorGenerator.WithResultName}<{CodeNames.TResultName}>()
        {{
            var config = Configurations.FirstOrDefault(conf => conf.ReturnType == typeof({CodeNames.TResultName}));

            if (config == null)
            {{
                throw new PolicyScopeMockException($""No matching policy scope mock configuration was found. Found no configuration with result type {{typeof(TResult)}}."", Configurations);
            }}

            return new {policyScopeRunnerMockGenerator.GetClassNameAndTemplateParamaters(codeNameWithResult)}(config);
        }}");

            sb.AppendLine(
$@"    }}");

            return sb.ToString();
        }
    }
}
