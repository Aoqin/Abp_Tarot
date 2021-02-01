using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EntityFrameworkCore;

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
      #region 使用 GetQueryableAsync
      var queryable = await cardCollectionRepository.GetQueryableAsync ();
      var result = queryable.Where (x => x.Id == id).Include (t => t.TarotCards);
      var collections = await AsyncExecuter.FirstOrDefaultAsync (result);
      #endregion

      #region 使用 EF CORE API
      // var dbContext = await cardCollectionRepository.GetDbContextAsync ();
      // var dbSet = dbContext.Set<TarotCardCollection> (); //Alternative, when you have the DbContext
      // // var dbSet = await cardCollectionRepository.GetDbSetAsync<TarotCardCollection> ();
      // var query = dbSet
      //   .Where (x => x.Id == id)
      //   .Include (x => x.TarotCards);
      // var collections = await AsyncExecuter.FirstOrDefaultAsync (query);
      #endregion

      #region 使用 EnsurePropertyLoadedAsync / EnsureCollectionLoadedAsync
      // var collections = await cardCollectionRepository.GetAsync (id, includeDetails : false);
      // await cardCollectionRepository.EnsureCollectionLoadedAsync (collections, x => x.TarotCards);
      #endregion

      #region 自定义默认 DefaultWithDetailsFunc
      // var collections = await cardCollectionRepository.GetAsync (id, includeDetails : true);
      #endregion

      #region 使用 widthdetails
      // var result = cardCollectionRepository.WithDetails (x => x.TarotCards);
      // var query = result.Where (c => c.Id == id);
      // var collection = await AsyncExecuter.FirstOrDefaultAsync (query);
      #endregion

      #region 使用 widthdetailsAsync
      // var result = await cardCollectionRepository.WithDetailsAsync (x => x.TarotCards);
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