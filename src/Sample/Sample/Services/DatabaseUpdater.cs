using System.Threading.Tasks;

namespace Sample.Services
{
    public class DatabaseUpdater : IDatabaseUpdater
    {
        public Task<int> UpdateAsync(MyDatabaseContext context)
        {
            return Task.FromResult(42);
        }
    }
}
