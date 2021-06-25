using System.Threading.Tasks;
using COSXML.Model;
using COSXML.Model.Service;
using EasyAbp.Abp.TencentCloud.COS;
using EasyAbp.Abp.TencentCloud.COS.Infrastructure;
using EasyAbp.Abp.TencentCloud.COS.QCloudImplement;
using Volo.Abp.Application.Services;

namespace Acme.Tarot.TencentCloud.Cos
{
  public class TencentCloudCosService : ApplicationService
  {
    // public IBucketOperator Operator;
    public CosServerWrapObject cosServerWrapObject;

    public TencentCloudCosService(CosServerWrapObject cosserverwrapobject)
    {
      this.cosServerWrapObject = cosserverwrapobject;
      // this.Operator = bucketOperator;
    }

    public Task<string> GetServicesAsync()
    {
      // var result = await Operator.CreateAsync("test-create");
      var reqeust = cosServerWrapObject.CosXmlServer.GetService(new GetServiceRequest());
      var result = reqeust.GetResultInfo();
      return Task.FromResult(result);
    }

    // public Task getAuthorizationAsync()
    // {
    //   var request = cosServerWrapObject.CosXmlServer;
    //   var result = request.GetResultInfo();
    // }
  }
}