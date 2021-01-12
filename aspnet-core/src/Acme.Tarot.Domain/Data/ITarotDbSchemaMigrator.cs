using System.Threading.Tasks;

namespace Acme.Tarot.Data
{
    public interface ITarotDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
