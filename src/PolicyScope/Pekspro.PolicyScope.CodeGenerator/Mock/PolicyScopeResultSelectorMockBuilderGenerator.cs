using Pekspro.PolicyScope.CodeGenerator.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pekspro.PolicyScope.CodeGenerator.Mock
{
    public class PolicyScopeResultSelectorMockBuilderGenerator : CodeGeneratorBase
    {
        public PolicyScopeResultSelectorMockBuilderGenerator()
            : base()
        {

        }

        public override string GetClassName(CodeNames codeNames)
        {
            string name = codeNames.ClassNamePrefix +
                            codeNames.ClassNameServicePrefix +
                            "PolicyScopeResultSelectorMockBuilder";

            return name;
        }

        public override bool WriteClassFileContent(int maxServiceCount)
        {
            string fileContent = CreateClassFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope.Mock/PolicyScopeResultSelectorMockBuilder.cs", fileContent);
        }

        public string CreateClassFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"namespace Pekspro.PolicyScope.Mock
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
            StringBuilder sb = new StringBuilder();

            sb.Append(
$@"    public class {GetClassNameAndTemplateParamaters(codeNames)}
    {{
        public {GetClassName(codeNames)}(PolicyScopeMockBuilder policyScopeMockBuilder)
        {{
            PolicyScopeMockBuilder = policyScopeMockBuilder;
        }}

        protected PolicyScopeMockBuilder PolicyScopeMockBuilder {{ get; }}

        public PolicyScopeMockBuilder WithNoResult()
        {{
            return PolicyScopeMockBuilder;
        }}

        public PolicyScopeMockBuilder WithResultType<TResult>()
        {{
            PolicyScopeMockBuilder.NextConfiguration.ReturnType = typeof(TResult);

            return PolicyScopeMockBuilder;
        }}
    }}
");

            return sb.ToString();
        }
    }
}
