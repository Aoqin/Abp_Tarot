using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.Tarot.Cards {
  public class TarotCardCollectionService : ApplicationService
  // CrudAppService<TarotCardCollection, TarotCardCollectionDto, Guid, PagedAndSortedResultRequestDto, TarotCardCollectionCreateDto, TarotCardCollectionUpdateDto> 
  {

    private readonly IRepository<TarotCardCollection, Guid> cardCollectionRepository;
    private readonly TarotCardCollectionManager manager;

    public TarotCardCollectionService (IRepository<TarotCardCollection, Guid> repository, TarotCardCollectionManager tarotCardCollectionManager) {
      cardCollectionRepository = repository;
      manager = tarotCardCollectionManager;
    }

    public async Task<TarotCardCollectionDto> FindByIdAsync (Guid id) {

      var collections = await cardCollectionRepository.GetAsync (id, includeDetails : true);
      #region widthdetails
      // var result = cardCollectionRepository.WithDetails (x => x.TarotCards);
      // var query = result.Where (c => c.Id == id);
      // var collection = await AsyncExecuter.FirstOrDefaultAsync (query);
      #endregion
      return ObjectMapper.Map<TarotCardCollection, TarotCardCollectionDto> (collections);
    }

    public async Task<List<TarotCardCollectionDto>> GetListAsync () {
      var collections = await manager.GetListAsync ();
      return ObjectMapper.Map<List<TarotCardCollection>, List<TarotCardCollectionDto>> (collections);
    }

    // public async List<TarotCardCollection> FindByNameWidthDetailsAsync (Guid tarotCardCollectionId) {
    //   var cardcollection = await cardCollectionRepository.GetAsync (c => c.Id == tarotCardCollectionId);
    //   var cardcollection = await cardCollectionRepository.GetAsync (x => x.Id == tarotCardCollectionId);
    //   return cardcollection;
    // }
  }
}