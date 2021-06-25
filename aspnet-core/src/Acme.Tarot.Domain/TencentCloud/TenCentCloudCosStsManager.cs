using Volo.Abp.DependencyInjection;
using COSSTS;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Acme.Tarot.TencentCloud.Cos.Sts
{
  public class TenCentCloudCosStsManager : ITransientDependency
  {
    public Dictionary<string, object> baseConfigs;
    public TenCentCloudCosStsManager(ServiceConfigurationContext context)
    {
            var configuration = context.Services.GetConfiguration();

            var appId = configuration["Settings:TencentCloud.AppId"];
            var secretId = configuration["Settings:TencentCloud.SecretId"];
            var secretKey = configuration["Settings:TencentCloud.SecretKey"];
            this.baseConfigs = new Dictionary<string, object>();
            this.baseConfigs.Add("secretId", secretId);
            this.baseConfigs.Add("secretKey", secretKey);
        }
   
    public Dictionary<string,object> genCredential(Dictionary<string, object> configDictinary)
        {
          
            Dictionary<string, object> values = new Dictionary<string, object>();
            foreach (var item in configDictinary)
            {
                values.Add(item.Key, item.Value);
            }
 
            foreach (var item in this.baseConfigs)
            {
                values.Add(item.Key, item.Value);
            }
 
            Dictionary<string, object> credential = STSClient.genCredential(values);
            return credential;
        }
    
  }
}