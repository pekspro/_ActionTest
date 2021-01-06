using System.Collections.Generic;
using System.Linq;

namespace Pekspro.PolicyScope.CodeGenerator.Common
{
    public class CodeNames
    {
        public CodeNames(int serviceCount, bool hasReturnType, bool isAsync)
        {
            HasReturnType = hasReturnType;
            IsAsync = isAsync;

            List<string> servicesTypes = new List<string>();
            List<string> servicesNames = new List<string>();

            if(serviceCount == 1)
            {
                servicesTypes.Add("T");
                servicesNames.Add("t");
            }
            else
            {
                for(int i = 1; i <= serviceCount; i++)
                {
                    servicesTypes.Add($"T{i}");
                    servicesNames.Add($"t{i}");
                }
            }

            ServicesTypes = servicesTypes;
            ServicesNames = servicesNames;
        }

        public bool HasServices => ServiceCount > 0;

        public int ServiceCount => ServicesTypes.Count();

        public IList<string> ServicesTypes { get; }

        public IList<string> ServicesNames { get; }

        public bool HasReturnType { get; }

        public bool IsAsync { get; }

        public string ClassNamePrefix => IsAsync ? "Async" : "Sync";

        public string BaseClassName => IsAsync ? "AsyncPolicyScopeBase" : "SyncPolicyScopeBase";
        
        public string ClassNameServicePrefix => HasServices ? "Service" : "NoService";
 
        public string ClassNameResultPrefix => HasReturnType ? "Result" : "NoResult";
        
        public string FunctionWithServicesName => ServiceCount == 0 ? "WithNoService" : ServiceCount == 1 ? "WithService" : "WithServices";

        public string TemplateParameters
        {
            get
            {
                if(!HasServices)
                {
                    return string.Empty;
                }

                return $"<{string.Join(", ", ServicesTypes)}>";
            }
        }

        public string TemplateParametersAndResult
        {
            get
            {
                if(!HasServices && !HasReturnType)
                {
                    return string.Empty;
                }

                List<string> par = new List<string>(ServicesTypes);
                if(HasReturnType)
                {
                    par.Add(TResultName);
                }

                return $"<{string.Join(", ", par)}>";
            }
        }

        public const string TResultName = "TResult";

        public string GetFunctionDeclaration()
        {
            string templateParameters = GetFunctionTemplateParameters();

            if (IsAsync || HasReturnType)
            {
                return "Func" + templateParameters;
            }
            else
            {
                return "Action" + templateParameters;
            }
        }


        public string GetFunctionTemplateParameters()
        {
            List<string> par = new List<string>(ServicesTypes);
            if (HasReturnType)
            {
                if (IsAsync)
                {
                    par.Add($"Task<{CodeNames.TResultName}>");
                }
                else
                {
                    par.Add(CodeNames.TResultName);
                }
            }
            else
            {
                if (IsAsync)
                {
                    par.Add($"Task");
                }
            }

            if (!par.Any())
            {
                return string.Empty;
            }

            return $"<{string.Join(", ", par)}>";
        }
    }
}
