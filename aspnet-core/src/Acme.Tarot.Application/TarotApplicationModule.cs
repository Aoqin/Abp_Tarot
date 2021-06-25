using EasyAbp.Abp.TencentCloud.Common;
using EasyAbp.Abp.TencentCloud.COS;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Acme.Tarot
{
  [DependsOn(
      typeof(TarotDomainModule),
      typeof(AbpAccountApplicationModule),
      typeof(TarotApplicationContractsModule),
      typeof(AbpIdentityApplicationModule),
      typeof(AbpPermissionManagementApplicationModule),
      typeof(AbpTenantManagementApplicationModule),
      typeof(AbpFeatureManagementApplicationModule),
      typeof(AbpTencentCloudCommonModule),
      typeof(AbpTencentCloudCOSModule)
      )]
  public class TarotApplicationModule : AbpModule
  {
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
      var configuration = context.Services.GetConfiguration();

      var appId = configuration["Settings:TencentCloud.AppId"];
      var secretId = configuration["Settings:TencentCloud.SecretId"];
      var secretKey = configuration["Settings:TencentCloud.SecretKey"];


      Configure<AbpAutoMapperOptions>(options =>
      {
        options.AddMaps<TarotApplicationModule>();
      });

      Configure<AbpTencentCloudCommonOptions>(options =>
      {
        options.AppId = appId;
        options.SecretId = secretId;
        options.SecretKey = secretKey;
      });

      Configure<AbpTencentCloudCOSOptions>(options =>
      {
        options.Region = "ap-chengdu";
      });
    }
  }
}
