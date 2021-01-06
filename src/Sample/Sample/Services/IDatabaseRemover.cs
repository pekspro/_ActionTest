using System.Threading.Tasks;

namespace Sample.Services
{
    public interface IDatabaseRemover
    {
        Task RemoveAsync(MyDatabaseContext context);
    }
}