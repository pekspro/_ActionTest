using Pekspro.PolicyScope.CodeGenerator.Common;
using System.Text;

namespace Pekspro.PolicyScope.CodeGenerator.MainLibrary
{
    public class PolicyScopeServiceSelectorGenerator : CodeGeneratorBase
    {
        public PolicyScopeServiceSelectorGenerator()
            : base()
        {

        }

        public override string GetClassName(CodeNames codeNames)
        {
            string name = codeNames.ClassNamePrefix + "PolicyScopeServiceSelector";

            return name;
        }

        public override bool WriteClassFileContent(int maxServiceCount)
        {
            string fileContent = CreateClassFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope/PolicyScopeServiceSelector.cs", fileContent);
        }

        public string CreateClassFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"using Polly;
using System;

namespace Pekspro.PolicyScope
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
            StringBuilder sb = new StringBuilder();

            string interfacePolicyName = codeNameBase.IsAsync ? "IAsyncPolicy" : "ISyncPolicy";
            string variablePolicyName = codeNameBase.IsAsync ? "asyncPolicy" : "syncPolicy";

            sb.Append(
$@"    internal class {GetClassNameAndTemplateParamaters(codeNameBase)} : {codeNameBase.BaseClassName}, {GetInterfaceNameAndTemplateParamaters(codeNameBase)}
    {{
        internal {GetClassName(codeNameBase)}(IServiceProvider serviceProvider, {interfacePolicyName} {variablePolicyName})
            : base(serviceProvider, {variablePolicyName})
        {{
        }}

");

            PolicyScopeResultSelectorGenerator servicePolicyScopeBuilderGenerator = new PolicyScopeResultSelectorGenerator();

            for (int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
            {
                if (serviceCount != 0)
                {
                    sb.AppendLine();
                }
                CodeNames codeNames = new CodeNames(serviceCount, false, codeNameBase.IsAsync);

                sb.AppendLine(
$@"        public {servicePolicyScopeBuilderGenerator.GetInterfaceNameAndTemplateParamaters(codeNames)} {codeNames.FunctionWithServicesName}{codeNames.TemplateParameters}()
        {{
            return new {servicePolicyScopeBuilderGenerator.GetClassNameAndTemplateParamaters(codeNames)}(this);
        }}");
            }

            sb.AppendLine(
$@"    }}");

            return sb.ToString();
        }


        public override bool WriteInterfaceFileContent(int maxServiceCount)
        {
            string fileContent = CreateInterfaceFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope/IPolicyScopeServiceSelector.cs", fileContent);
        }

        public string CreateInterfaceFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"namespace Pekspro.PolicyScope
{");

            for(int asyncMode = 0; asyncMode < (GenerateSync ? 2 : 1); asyncMode++)
            {
                sb.AppendLine(CreateInterfaceCode(new CodeNames(0, false, asyncMode == 0), maxServiceCount));
            }

            sb.Remove(sb.Length - 2, 2);

            sb.AppendLine(@"}");

            return sb.ToString();
        }

        public string CreateInterfaceCode(CodeNames codeNameBase, int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(
$@"    public interface {GetInterfaceNameAndTemplateParamaters(codeNameBase)}
    {{
");
            PolicyScopeResultSelectorGenerator servicePolicyScopeBuilderGenerator = new PolicyScopeResultSelectorGenerator();

            for (int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
            {
                if(serviceCount != 0)
                {
                    sb.AppendLine();
                }

                CodeNames codeNames = new CodeNames(serviceCount, false, codeNameBase.IsAsync);

                sb.AppendLine(
$@"        {servicePolicyScopeBuilderGenerator.GetInterfaceNameAndTemplateParamaters(codeNames)} {codeNames.FunctionWithServicesName}{codeNames.TemplateParameters}();");
            }

            sb.AppendLine(
$@"    }}");

            return sb.ToString();
        }
    }
}
