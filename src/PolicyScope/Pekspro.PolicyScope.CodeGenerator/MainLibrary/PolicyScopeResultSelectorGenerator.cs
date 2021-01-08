using Pekspro.PolicyScope.CodeGenerator.Common;
using System.Text;

namespace Pekspro.PolicyScope.CodeGenerator.MainLibrary
{
    public class PolicyScopeResultSelectorGenerator : CodeGeneratorBase
    {
        public const string WithNoResultName = "WithNoResult";
        public const string WithResultName = "WithResult";

        public PolicyScopeResultSelectorGenerator()
            : base()
        {

        }

        public override string GetClassName(CodeNames codeNames)
        {
            string name = codeNames.ClassNamePrefix +
                            "PolicyScopeResultSelector";

            return name;
        }

        public override bool WriteClassFileContent(int maxServiceCount)
        {
            string fileContent = CreateClassFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope/PolicyScopeResultSelector.cs", fileContent);
        }

        public string CreateClassFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"namespace Pekspro.PolicyScope
{");

            for (int asyncMode = 0; asyncMode < (GenerateSync ? 2 : 1); asyncMode++)
            {
                for (int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
                {
                    sb.AppendLine(CreateClassCode(new CodeNames(serviceCount, false, asyncMode == 0)));
                }
            }

            sb.AppendLine(@"}");

            return sb.ToString();
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
            CodeNames codeNameWithResult = new CodeNames(codeNames.ServiceCount, true, codeNames.IsAsync);

            PolicyScopeRunnerGenerator policyRunnerGenerator = new PolicyScopeRunnerGenerator();

            sb.AppendLine(
$@"        public {policyRunnerGenerator.GetInterfaceNameAndTemplateParamaters(codeNames)} {WithNoResultName}()
        {{
            return new {policyRunnerGenerator.GetClassNameAndTemplateParamaters(codeNames)}(this);
        }}

        public {policyRunnerGenerator.GetInterfaceNameAndTemplateParamaters(codeNameWithResult)} {WithResultName}<{CodeNames.TResultName}>()
        {{
            return new {policyRunnerGenerator.GetClassNameAndTemplateParamaters(codeNameWithResult)}(this);
        }}");

            sb.AppendLine(
$@"    }}");

            return sb.ToString();
        }


        public override bool WriteInterfaceFileContent(int maxServiceCount)
        {
            string fileContent = CreateInterfaceFileContent(maxServiceCount);

            return WriteFileContent("Pekspro.PolicyScope/IPolicyScopeResultSelector.cs", fileContent);
        }

        public string CreateInterfaceFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"namespace Pekspro.PolicyScope
{");

            for(int asyncMode = 0; asyncMode < (GenerateSync ? 2 : 1); asyncMode++)
            {
                for(int serviceCount = 0; serviceCount <= maxServiceCount; serviceCount++)
                {
                    sb.AppendLine(CreateInterfaceCode(new CodeNames(serviceCount, false, asyncMode == 0)));
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
            CodeNames codeNameWithResult = new CodeNames(codeNames.ServiceCount, true, codeNames.IsAsync);

            PolicyScopeRunnerGenerator policyRunnerGenerator = new PolicyScopeRunnerGenerator();

            sb.AppendLine(
$@"        {policyRunnerGenerator.GetInterfaceNameAndTemplateParamaters(codeNames)} WithNoResult();
");

            sb.AppendLine(
$@"        {policyRunnerGenerator.GetInterfaceNameAndTemplateParamaters(codeNameWithResult)} WithResult<{CodeNames.TResultName}>();");

            sb.AppendLine(
$@"    }}");

            return sb.ToString();
        }
    }
}
