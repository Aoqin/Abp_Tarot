using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.Tarot.Cards {
  public class TarotCardSolutionService : ApplicationService {
    public IRepository<TarotCardSolution> TarotCardSolutions { get; set; }
    public IRepository<TarotCardCollection, Guid> TarotCardCollections { get; set; }

    public TarotCardSolutionService (IRepository<TarotCardSolution> tarotCardSolutions, IRepository<TarotCardCollection, Guid> tarotCardCollections) {
      TarotCardSolutions = tarotCardSolutions;
      TarotCardCollections = tarotCardCollections;
    }

    // public async Task<List<TarotCardSolution>> GetAllAsync (Guid id) {
    //   var result = await TarotCardSolutions.GetQueryableAsync (true, x => x.TarotCardCollectionId == id);
    //   return result;
    // }
    public async Task<TarotCardSolution> GetAsync (Guid id, List<Guid> tarotCardIds) {
      var result = await TarotCardSolutions.GetAsync (x => x.TarotCardCollectionId == id);
      return result;
    }

    public async Task<TarotCardSolution> GetAsyncWithDetails (Guid id, List<Guid> tarotCardIds) {
      var result = await TarotCardSolutions.GetAsync (x => x.TarotCardCollectionId == id);
      return result;
    }

    public async Task<TarotCardSolution> CreateAsync (Guid id, TarotCardSolutionCreateDto tarotCardSolutionCreateDto) {
      Check.NotNull (id, nameof (id));
      Check.NotNull (tarotCardSolutionCreateDto, nameof (tarotCardSolutionCreateDto));
      var lam = await TarotCardCollections.WithDetailsAsync (x => x.TarotCards);
      var queryable = lam.Where (x => x.Id == id);
      var collection = await AsyncExecuter.FirstOrDefaultAsync (queryable);
      if (collection == null) {
        throw new UserFriendlyException (@"tarotcard collection is null");
      } else {
        if (tarotCardSolutionCreateDto.TarotCardIds.Count != collection.DivinationLimit) {
          throw new UserFriendlyException ($"divination need {collection.DivinationLimit} cards");
        }
        tarotCardSolutionCreateDto.TarotCardIds.Sort ();
        foreach (var itemId in tarotCardSolutionCreateDto.TarotCardIds) {
          var tarotcard = collection.TarotCards.ToList ().Find (x => x.Id == itemId);
          if (tarotcard == null) {
            throw new UserFriendlyException ($"tarotcard {itemId} not in collection");
          }
        }

        string cardIdsToString = string.Join (",", tarotCardSolutionCreateDto.TarotCardIds.ToArray ());
        var solution = await TarotCardSolutions.GetAsync (x => x.TarotCardCollectionId == id && x.TarotCardIdsToString == cardIdsToString);
        if (solution != null) {
          throw new UserFriendlyException ("solution is already existed");
        }
        var newSolution = new TarotCardSolution () {
          TarotCardCollectionId = collection.Id,
          TarotCardIdsToString = cardIdsToString
        };
        var result = await TarotCardSolutions.InsertAsync (newSolution);
        return result;
        // var solution = await TarotCardSolutions.GetAsync (x => x.TarotCardCollectionId == id && x.TarotCardIdsToString == cardIdsToString);
      }
    }
  }
}