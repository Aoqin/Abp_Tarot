using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace Acme.Tarot.Cards {
    public class CardBindCollectionDataSeedContributor : IDataSeedContributor, ITransientDependency {
        private readonly IRepository<TarotCard, Guid> TarotCards;
        private readonly IRepository<TarotCardCollection, Guid> TarotCardCollections;

        private readonly IRepository<CardBindCollection> CardBindCollections;
        private readonly ICurrentTenant CurrentTenant;
        public CardBindCollectionDataSeedContributor (
            IRepository<TarotCard, Guid> tarotCards,
            IRepository<TarotCardCollection, Guid> tarotCardCollections,
            IRepository<CardBindCollection> cardBindCollections,
            ICurrentTenant currentTenant
        ) {
            TarotCardCollections = tarotCardCollections;
            TarotCards = tarotCards;
            CardBindCollections = cardBindCollections;
            CurrentTenant = currentTenant;
        }
        public async Task SeedAsync (DataSeedContext context) {
            using (CurrentTenant.Change (context?.TenantId)) {
                if (await CardBindCollections.GetCountAsync () > 0) {
                    return;
                }
                var tarotcard = await TarotCards.SingleOrDefaultAsync ();
                var tarotCardCollection = await TarotCardCollections.SingleOrDefaultAsync ();
                tarotCardCollection.TarotCards.Add (tarotcard);
                await TarotCardCollections.UpdateAsync (tarotCardCollection);
            }

        }
    }
}