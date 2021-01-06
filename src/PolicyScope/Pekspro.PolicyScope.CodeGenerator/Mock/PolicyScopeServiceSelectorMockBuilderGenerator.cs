using Pekspro.PolicyScope.CodeGenerator.Common;
using Pekspro.PolicyScope.CodeGenerator.MainLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pekspro.PolicyScope.CodeGenerator.Mock
{
    public class PolicyScopeServiceSelectorMockBuilderGenerator : CodeGeneratorBase
    {
        public PolicyScopeServiceSelectorMockBuilderGenerator()
            : base()
        {

        }

        public override string GetClassName(CodeNames codeNames)
        {
            string name = codeNames.ClassNamePrefix +
                            "PolicyScopeServiceSelectorMockBuilder";

            return name;
        }

        public override void WriteClassFileContent(int maxServiceCount)
        {
            string fileContent = CreateClassFileContent(maxServiceCount);

            WriteFileContent("Pekspro.PolicyScope.Mock/PolicyScopeServiceSelectorMockBuilder.cs", fileContent);
        }

        public string CreateClassFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"namespace Pekspro.PolicyScope.Mock
{");

            for (int asyncMode = 0; asyncMode < (GenerateSync ? 2 : 1); asyncMode++)
            {
                sb.AppendLine(CreateClassCode(new CodeNames(0, false, asyncMode == 0), maxServiceCount));
            }

            sb.Remove(sb.Length - 2, 2);

            sb.AppendLine(@"}");

            return sb.ToString();
        }

        public string CreateClassCode(CodeNames codeNames, int maxServiceCount)
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
");

            PolicyScopeResultSelectorMockBuilderGenerator policyScopeResultSelectorMockGenerator =
                new PolicyScopeResultSelectorMockBuilderGenerator();

            for (int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
            {
                CodeNames codeNamesWithService = new CodeNames(serviceCount, false, codeNames.IsAsync);

                string constructorServiceParameters = string.Empty;
                string configurationCode = string.Empty;

                if (codeNamesWithService.HasServices)
                {
                    if (codeNamesWithService.ServiceCount == 1)
                    {
                        constructorServiceParameters = $"{codeNamesWithService.ServicesTypes.First()} service";
                        configurationCode =
$@"
            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof({codeNamesWithService.ServicesTypes.First()}));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service!);
";

                    }
                    else
                    {
                        configurationCode =
$@"
";
                        for (int i = 1; i <= codeNamesWithService.ServiceCount; i++)
                        {
                            if(i != 1)
                            {
                                constructorServiceParameters += ", ";
                            }

                            constructorServiceParameters += $"{codeNamesWithService.ServicesTypes[i - 1]} service{i}";
                            configurationCode +=
$@"            PolicyScopeMockBuilder.NextConfiguration.ServiceTypes.Add(typeof({codeNamesWithService.ServicesTypes[i - 1]}));
            PolicyScopeMockBuilder.NextConfiguration.ServiceInstances.Add(service{i}!);
";
                        }
                    }
                }

                sb.Append(
$@"

        public {policyScopeResultSelectorMockGenerator.GetClassNameAndTemplateParamaters(codeNamesWithService)} {codeNamesWithService.FunctionWithServicesName}{codeNamesWithService.TemplateParameters}({constructorServiceParameters})
        {{{configurationCode}
            return new {policyScopeResultSelectorMockGenerator.GetClassNameAndTemplateParamaters(codeNamesWithService)}(PolicyScopeMockBuilder);
        }}");
            }


            sb.Append(
$@"
    }}
");
            return sb.ToString();
        }
    }
}
