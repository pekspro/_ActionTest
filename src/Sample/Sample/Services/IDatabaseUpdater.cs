using System.Threading.Tasks;

namespace Sample.Services
{
    public interface IDatabaseUpdater
    {
        Task<int> UpdateAsync(MyDatabaseContext context);
    }
}