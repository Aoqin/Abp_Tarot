using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Acme.Tarot.Cards {
    public class TarotCardDataSeedContributor : IDataSeedContributor, ITransientDependency {

        private readonly IRepository<TarotCard, Guid> _tarotcardRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;

        public TarotCardDataSeedContributor (IRepository<TarotCard, Guid> tarotCardRepository, IGuidGenerator guidGenerator, ICurrentTenant currentTenant) {
            _tarotcardRepository = tarotCardRepository;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync (DataSeedContext context) {
            using (_currentTenant.Change (context?.TenantId)) {
                if (await _tarotcardRepository.GetCountAsync () > 0) {
                    return;
                }
                var tarotCard = new TarotCard {
                    // Id = _guidGenerator.Create()
                    Name = "test",
                    Descript = "没有什么的描述",
                    CardFrontImgUrl = "",
                    CardContentText = "test",
                };
                await _tarotcardRepository.InsertAsync (tarotCard, autoSave : true);
            }
        }
    }
}