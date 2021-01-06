using System.Threading.Tasks;

namespace Sample.Workers
{
    public interface IWithoutPolicyScope
    {
        public Task<int> UpdateWithoutPolicyScopeAsync();
    }
}
