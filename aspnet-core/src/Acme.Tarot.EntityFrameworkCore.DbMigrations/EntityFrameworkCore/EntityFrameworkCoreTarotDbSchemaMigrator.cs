using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Acme.Tarot.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.Tarot.EntityFrameworkCore
{
    public class EntityFrameworkCoreTarotDbSchemaMigrator
        : ITarotDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreTarotDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the TarotMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<TarotMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}