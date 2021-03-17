using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.Tarot.Cards {
  public class TarotCardSolutionService : ApplicationService {
    public IRepository<TarotCardSolution> TarotCardSolutions { get; set; }
    public IRepository<TarotCardCollection, Guid> TarotCardCollections { get; set; }

    public IRepository<TarotCard, Guid> TarotCards { get; set; }
    public TarotCardSolutionService (IRepository<TarotCardSolution> tarotCardSolutions, IRepository<TarotCardCollection, Guid> tarotCardCollections, IRepository<TarotCard, Guid> tarotCards) {
      TarotCardSolutions = tarotCardSolutions;
      TarotCardCollections = tarotCardCollections;
      TarotCards = tarotCards;
    }

    // public async Task<List<TarotCardSolution>> GetAllAsync (Guid id) {
    //   var result = await TarotCardSolutions.GetQueryableAsync (true, x => x.TarotCardCollectionId == id);
    //   return result;
    // }
    public async Task<TarotCardSolution> GetAsync (Guid id, List<Guid> tarotCardIds) {
      tarotCardIds.Sort ();
      var result = await TarotCardSolutions.GetAsync (x => x.TarotCardCollectionId == id && x.TarotCardIds.Equals (tarotCardIds));
      return result;
    }

    public async Task<TarotCardSolution> GetAsyncWithDetails (Guid id, List<Guid> tarotCardIds) {
      tarotCardIds.Sort ();

      TarotCardSolution result = await TarotCardSolutions.GetAsync (x => x.TarotCardCollectionId == id && x.TarotCardIds.Equals (tarotCardIds));
      if (result != null) {
        result.TarotCardCollection = await TarotCardCollections.GetAsync (x => x.Id == id);
        foreach (var i in result.TarotCardIds) {
          // Console.WriteLine (i);
          var card = await TarotCards.GetAsync (x => x.Id == i);
          if (card != null) {
            result.AddCards (card);
          }
        }
      } else {
        throw new UserFriendlyException ($"不存在");
      }
      return result;
    }

    public async Task<TarotCardSolution> CreateAsync (TarotCardSolutionCreateDto tarotCardSolutionCreateDto) {
      Check.NotNull (tarotCardSolutionCreateDto, nameof (tarotCardSolutionCreateDto));
      tarotCardSolutionCreateDto.TarotCardIds.Sort ();

      var lam = await TarotCardCollections.WithDetailsAsync (x => x.TarotCards);
      var queryable = lam.Where (x => x.Id == tarotCardSolutionCreateDto.TarotCardCollectionId);

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

        var solution = await TarotCardSolutions
          .FindAsync (
            x => x.TarotCardCollectionId == tarotCardSolutionCreateDto.TarotCardCollectionId &&
            x.TarotCardIds.Equals (tarotCardSolutionCreateDto.TarotCardIds)
          );

        if (solution != null) {
          throw new UserFriendlyException ("solution is already existed");
        }

        var newSolution = new TarotCardSolution () {
          TarotCardCollectionId = collection.Id,
        };

        var result = await TarotCardSolutions.InsertAsync (newSolution);
        return result;
      }
    }

    public async Task<TarotCardSolution> UpdateAsync (TarotCardSolutionUpdateDto tarotCardSolutionUpdateDto) {
      Check.NotNull (tarotCardSolutionUpdateDto, nameof (tarotCardSolutionUpdateDto));
      tarotCardSolutionUpdateDto.TarotCardIds.Sort ();

      var collection = await TarotCardCollections.GetAsync (x => x.Id == tarotCardSolutionUpdateDto.TarotCardCollectionId);
      if (collection.DivinationLimit != tarotCardSolutionUpdateDto.TarotCardIds.Count) {
        throw new UserFriendlyException ($"need {collection.DivinationLimit} cards");
      }

      var solution = await TarotCardSolutions.GetAsync (
        x => x.TarotCardCollectionId == tarotCardSolutionUpdateDto.TarotCardCollectionId &&
        x.TarotCardIds.Equals (tarotCardSolutionUpdateDto.TarotCardIds)
      );
      var newSolution = new TarotCardSolution () {
        TarotCardCollectionId = tarotCardSolutionUpdateDto.TarotCardCollectionId,
        TarotCardIds = tarotCardSolutionUpdateDto.TarotCardIds,
        Hexagram = tarotCardSolutionUpdateDto.Hexagram,
        HexagramExplain = tarotCardSolutionUpdateDto.HexagramExplain
      };
      var updatedSolution = await TarotCardSolutions.UpdateAsync (newSolution);
      return updatedSolution;
    }

    public async Task<bool> DeleteAsync ([NotNull] Guid collectionId, [NotNull] List<Guid> tarotCardIds) {
      tarotCardIds.Sort ();
      Check.NotNullOrEmpty (collectionId.ToString (), nameof (collectionId));
      Check.NotNullOrEmpty (tarotCardIds, nameof (tarotCardIds));
      await TarotCardSolutions.GetAsync (x => x.TarotCardCollectionId == collectionId && x.TarotCardIds.Equals (tarotCardIds));
      await TarotCardSolutions.DeleteAsync (
        x => x.TarotCardCollectionId == collectionId &&
        x.TarotCardIds.Equals (tarotCardIds)
      );
      return true;
    }

  }
}