using System.Threading.Tasks;

namespace Sample.Services
{
    public class DatabaseRemover : IDatabaseRemover
    {
        public Task RemoveAsync(MyDatabaseContext context)
        {
            return Task.CompletedTask;
        }
    }
}
