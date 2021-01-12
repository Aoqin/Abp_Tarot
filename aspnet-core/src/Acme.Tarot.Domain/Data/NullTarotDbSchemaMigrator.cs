using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Acme.Tarot.Data
{
    /* This is used if database provider does't define
     * ITarotDbSchemaMigrator implementation.
     */
    public class NullTarotDbSchemaMigrator : ITarotDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}