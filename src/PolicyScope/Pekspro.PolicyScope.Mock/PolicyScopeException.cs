using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pekspro.PolicyScope.Mock
{
    public class PolicyScopeMockException : Exception
    {
        public PolicyScopeMockException(string message)
            : base(message)
        {
        }

        internal PolicyScopeMockException(string message, IEnumerable<PolicyScopeMockConfiguration> configurations)
            : base(CreateExeptionMessage(message, configurations))
        {
        }

        static string CreateExeptionMessage(string message, IEnumerable<PolicyScopeMockConfiguration> configurations)
        {
            StringBuilder sb = new StringBuilder(message);

            if(configurations.Any())
            {
                sb.AppendLine();
                sb.AppendLine();

                if(configurations.Count() == 1)
                {
                    sb.AppendLine($"One configuration availible:");
                }
                else
                {
                    sb.AppendLine($"{configurations.Count()} configuration availible:");
                }

                foreach(var config in configurations)
                {
                    sb.AppendLine();
                    if(config.IsAsync)
                    {
                        sb.Append("Async");
                    }
                    else
                    {
                        sb.Append("Sync");
                    }

                    sb.AppendLine($" policy with name: {config.PolicyName}");
                    sb.Append($"Return type: ");
                    if (config.ReturnType == null)
                    {
                        sb.Append("void");
                    }
                    else
                    {
                        sb.Append(config.ReturnType.ToString());
                    }
                    sb.AppendLine();

                    sb.Append($"Services: ");
                    if (config.ServiceTypes.Count() == 0)
                    {
                        sb.AppendLine("None");
                    }
                    else
                    {
                        sb.AppendLine(config.ServiceTypes.Count().ToString());
                        foreach(var t in config.ServiceTypes)
                        {
                            sb.AppendLine(t.ToString());
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }
}
