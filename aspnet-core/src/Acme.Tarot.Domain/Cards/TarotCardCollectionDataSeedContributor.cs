using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Acme.Tarot.Cards {
  public class TarotCardCollectionDataSeedContributor : IDataSeedContributor, ITransientDependency {
    private readonly IRepository<TarotCardCollection, Guid> TarotCardCollectionRepository;
    private readonly IGuidGenerator GuidGenerator;
    private readonly ICurrentTenant CurrentTenant;

    public TarotCardCollectionDataSeedContributor (IRepository<TarotCardCollection, Guid> tarotCardCollections, IGuidGenerator guidGenerator, ICurrentTenant currentTenant) {
      TarotCardCollectionRepository = tarotCardCollections;
      GuidGenerator = guidGenerator;
      CurrentTenant = currentTenant;
    }

    public async Task SeedAsync (DataSeedContext context) {
      using (CurrentTenant.Change (context?.TenantId)) {
        if (await TarotCardCollectionRepository.GetCountAsync () > 0) {
          return;
        }
        var tarotCardCollection = new TarotCardCollection () {
          Name = "test_collection",
          Descript = "新建测试卡牌套",
          CardBackImgUrl = "",
          CardFrontImgUrl = ""
        };
        await TarotCardCollectionRepository.InsertAsync (tarotCardCollection);
      }
    }
  }
}