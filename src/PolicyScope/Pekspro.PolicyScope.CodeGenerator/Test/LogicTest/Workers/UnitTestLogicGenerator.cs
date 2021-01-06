using Pekspro.PolicyScope.CodeGenerator.Common;
using Pekspro.PolicyScope.CodeGenerator.LogicTest.Workers;
using System;
using System.Linq;
using System.Text;

namespace Pekspro.PolicyScope.CodeGenerator.Test.LogicTest.Workers
{
    public class UnitTestLogicGenerator : CodeGeneratorBase
    {
        public UnitTestLogicGenerator()
            : base()
        {

        }

        public override string GetClassName(CodeNames codeNames)
        {
            LogicGenerator dummyLogicGenerator = new LogicGenerator();

            string name = "UnitTest" + dummyLogicGenerator.GetClassName(codeNames);

            return name;
        }

        public override void WriteClassFileContent(int maxServiceCount)
        {
            string fileContent = CreateClassFileContent(maxServiceCount);

            WriteFileContent("Pekspro.PolicyScope.Test/LogicTest/Workers/UnitTestLogic.cs", fileContent);
        }

        public string CreateClassFileContent(int maxServiceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"using Moq;
using Pekspro.PolicyScope.LogicTest;
using Pekspro.PolicyScope.LogicTest.Services;
using Pekspro.PolicyScope.LogicTest.Workers;
using Pekspro.PolicyScope.Mock;
using System.Threading.Tasks;
using Xunit;

namespace Pekspro.PolicyScope.Test.LogicTest.Workers
{{
    public class {GetClassName(new CodeNames(0, false, false))}
    {{");

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

            sb.Remove(sb.Length - 2, 2);

            sb.AppendLine(@"    }
}");

            return sb.ToString();
        }

        public static string GetRunName(bool isAsync)
        {
            if (isAsync)
            {
                return "RunAsync";
            }
            else
            {
                return "Run";
            }
        }

        public string CreateFunctionDeclaration(CodeNames codeNames)
        {
            var dummyLogicGenerator = new LogicGenerator();

            string s = string.Empty;

            if (codeNames.IsAsync)
            {
                s += "async Task ";
            }
            else
            {
                s += "void ";
            }

            return s + "Test" + dummyLogicGenerator.CreateFunctionName(codeNames);
        }

        public string CreateFunctionCode(CodeNames codeNames)
        {
            LogicGenerator logicGenerator = new LogicGenerator();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($@"        [Fact]");
            sb.AppendLine($@"        public {CreateFunctionDeclaration(codeNames)}()");

            sb.AppendLine(
$@"        {{
            // Arrange");

            var serviceRange = Enumerable
                .Range(1, codeNames.ServiceCount);

            if (codeNames.IsAsync)
            {
                var mockDeclarations = serviceRange
                    .Select(r =>
$@"            Mock<IDummyService{r}> dummyServiceMock{r} = new Mock<IDummyService{r}>();
            dummyServiceMock{r}
                .Setup(d => d.CalculateAsync())
                .Returns(Task.FromResult(1 << {r - 1}));"
                );

                sb.Append(string.Join(Environment.NewLine + Environment.NewLine, mockDeclarations));
            }
            else
            {
                var mockDeclarations = serviceRange
    .Select(r =>
$@"            Mock<IDummyService{r}> dummyServiceMock{r} = new Mock<IDummyService{r}>();
            dummyServiceMock{r}
                .Setup(d => d.Calculate())
                .Returns(1 << {r - 1});"
);

                sb.Append(string.Join(Environment.NewLine + Environment.NewLine, mockDeclarations));
            }

            if (codeNames.HasServices)
            {
                sb.AppendLine();
                sb.AppendLine();
            }

            if (codeNames.IsAsync)
            {
                sb.AppendLine(@"            var policyScopeMock = PolicyScopeMock
                            .AsyncPolicy(PolicyNames.AsyncPolicyName)");
            }
            else
            {
                sb.AppendLine(@"            var policyScopeMock = PolicyScopeMock
                            .SyncPolicy(PolicyNames.SyncPolicyName)");
            }

            if (codeNames.HasServices)
            {
                sb.Append($"                            .{codeNames.FunctionWithServicesName}(");

                var mockNames = serviceRange
                    .Select(r =>
    $@"dummyServiceMock{r}.Object"
                    );

                sb.Append(string.Join(", ", mockNames));
                sb.AppendLine(")");
            }
            else
            {
                sb.AppendLine($"                            .{codeNames.FunctionWithServicesName}()");
            }

            if (codeNames.HasReturnType)
            {
                sb.AppendLine("                            .WithResultType<int>()");
            }
            else
            {
                sb.AppendLine("                            .WithNoResult()");
            }

            sb.AppendLine("                            .Build();");


            sb.AppendLine();
            sb.AppendLine($"            {logicGenerator.GetClassName(codeNames)} logic = new {logicGenerator.GetClassName(codeNames)}(policyScopeMock.Object);");
            sb.AppendLine();
            sb.AppendLine("            // Act");

            LogicGenerator dummyLogicGenerator = new LogicGenerator();

            if (codeNames.IsAsync)
            {
                if (codeNames.HasReturnType)
                {
                    sb.AppendLine($"            int result = await logic.{dummyLogicGenerator.CreateFunctionName(codeNames)}();");
                }
                else
                {
                    sb.AppendLine($"            await logic.{dummyLogicGenerator.CreateFunctionName(codeNames)}();");
                }
            }
            else
            {
                if (codeNames.HasReturnType)
                {
                    sb.AppendLine($"            int result = logic.{dummyLogicGenerator.CreateFunctionName(codeNames)}();");
                }
                else
                {
                    sb.AppendLine($"            logic.{dummyLogicGenerator.CreateFunctionName(codeNames)}();");
                }
            }

            sb.AppendLine();
            sb.AppendLine("            // Assert");
            string expectedResult = $"(1 << {codeNames.ServiceCount}) - 1";

            if (codeNames.HasReturnType)
            {
                sb.AppendLine($"            Assert.Equal({expectedResult}, result);");
            }
            sb.AppendLine($"            Assert.Equal({expectedResult}, logic.LatestResult);");
            sb.AppendLine($"            policyScopeMock.VerifyRunOnce();");

            sb.AppendLine(
@"        }");

            return sb.ToString();
        }
    }
}
