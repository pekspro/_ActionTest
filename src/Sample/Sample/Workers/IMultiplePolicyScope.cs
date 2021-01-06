using System.Threading.Tasks;

namespace Sample.Workers
{
    public interface IMultiplePolicyScope
    {
        Task<int> UseMultipleNestedScopesAsync();
    }
}