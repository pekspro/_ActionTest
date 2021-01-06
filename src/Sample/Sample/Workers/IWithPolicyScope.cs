using System.Threading.Tasks;

namespace Sample.Workers
{
    public interface IWithPolicyScope
    {
        public Task<int> UpdateWithPolicyScopeAsync();
    }
}
