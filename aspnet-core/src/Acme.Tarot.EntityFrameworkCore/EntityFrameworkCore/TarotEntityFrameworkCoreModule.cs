using Acme.Tarot.Cards;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Acme.Tarot.EntityFrameworkCore {
    [DependsOn (
        typeof (TarotDomainModule),
        typeof (AbpIdentityEntityFrameworkCoreModule),
        typeof (AbpIdentityServerEntityFrameworkCoreModule),
        typeof (AbpPermissionManagementEntityFrameworkCoreModule),
        typeof (AbpSettingManagementEntityFrameworkCoreModule),
        typeof (AbpEntityFrameworkCoreSqlServerModule),
        typeof (AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof (AbpAuditLoggingEntityFrameworkCoreModule),
        typeof (AbpTenantManagementEntityFrameworkCoreModule),
        typeof (AbpFeatureManagementEntityFrameworkCoreModule)
    )]
    public class TarotEntityFrameworkCoreModule : AbpModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            TarotEfCoreEntityExtensionMappings.Configure ();
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddAbpDbContext<TarotDbContext> (options => {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories (includeAllEntities: true);
                // options.AddRepository<TarotCardCollection, TarotRepository> ();
            });

            Configure<AbpDbContextOptions> (options => {
                /* The main point to change your DBMS.
                 * See also TarotMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer ();
                // options.Entity<TarotCardCollection> (orderOptions => {
                //     orderOptions.DefaultWithDetailsFunc = query => query.Include (o => o.TarotCards);
                // });
            });
        }
    }
}