using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Acme.Tarot.EntityFrameworkCore
{
    [DependsOn(
        typeof(TarotEntityFrameworkCoreModule)
        )]
    public class TarotEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<TarotMigrationsDbContext>();
        }
    }
}
