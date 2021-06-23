using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
// using EasyAbp.Abp.TencentCloud.COS;
// using EasyAbp.Abp.TencentCloud.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Tarot
{
    [DependsOn(
        typeof(TarotDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(TarotApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule)
        // typeof(AbpTencentCloudCommonModule),
        // typeof(AbpTencentCloudCOSModule)
        )]
    public class TarotApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            // const appId = configuration[""]


            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<TarotApplicationModule>();
            });

            // Configure<AbpTencentCloudCommonOptions>(options =>
            // {
            //     options.AppId = "1259787355";
            //     options.SecretId = "AKIDxlKcdNr51g6FaeS0IJ3WidHekZ7PQAeq";
            //     options.SecretKey = "xd2A1buwiESP5VeW7ankQs4ze3B2iBNJ";
            // });

            // Configure<AbpTencentCloudCOSOptions>(options =>
            // {
            //     options.Region = "ap-chengdu";
            //     options.ConnectionTimeout = 1000 * 60 * 15;
            //     options.KeyDurationSecond = 1000 * 60 * 60;
            //     options.ReadWriteTimeout = 1000 * 60 * 60;
            // });
        }
    }
}
