using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.Tarot.Cards {
  public class TarotCardCollectionService : CrudAppService<TarotCardCollection, TarotCardCollectionDto, Guid, PagedAndSortedResultRequestDto, TarotCardCollectionCreateDto, TarotCardCollectionUpdateDto> {

    private readonly IRepository<TarotCardCollection, Guid> cardCollectionRepository;
    private readonly IRepository<TarotCard, Guid> tarotCardRepository;
    private readonly TarotCardCollectionManager manager;

    public TarotCardCollectionService (
      IRepository<TarotCardCollection, Guid> repository,
      IRepository<TarotCard, Guid> cardrepository,
      TarotCardCollectionManager tarotCardCollectionManager
    ) : base (repository) {
      cardCollectionRepository = repository;
      tarotCardRepository = cardrepository;
      manager = tarotCardCollectionManager;
    }

    public override async Task<TarotCardCollectionDto> GetAsync (Guid id) {
      var collection = await cardCollectionRepository.GetAsync (id);
      await cardCollectionRepository.EnsureCollectionLoadedAsync (collection, x => x.TarotCards);
      return ObjectMapper.Map<TarotCardCollection, TarotCardCollectionDto> (collection);
    }

    // public async Task<TarotCardCollectionDto> CreateAsync (TarotCardCollectionCreateDto tarotCardCollectionCreateDto) {
    //   var count = await cardCollectionRepository.CountAsync (x => x.Name == tarotCardCollectionCreateDto.Name);
    //   if (count > 0) {
    //     throw new BusinessException (@"tarotCardCollectionCreateDto.Name 已存在");
    //   }
    //   var collection = new TarotCardCollection () {
    //     Name = tarotCardCollectionCreateDto.Name,
    //     Descript = tarotCardCollectionCreateDto.Descript,
    //     CardFrontImgUrl = tarotCardCollectionCreateDto.CardFrontImgUrl,
    //     CardBackImgUrl = tarotCardCollectionCreateDto.CardBackImgUrl
    //   };
    //   var result = await cardCollectionRepository.InsertAsync (collection);
    //   return ObjectMapper.Map<TarotCardCollection, TarotCardCollectionDto> (result);
    // }

    // public async Task<TarotCardCollectionDto> UpdateAsync (Guid id, TarotCardCollectionUpdateDto tarotCardCollectionUpdateDto) {
    //   var cardcollection = await cardCollectionRepository.GetAsync (x => x.Id == id);
    //   var result = await cardCollectionRepository.CountAsync (x => x.Name == tarotCardCollectionUpdateDto.Name && x.Name != cardcollection.Name);
    //   if (result > 0) {
    //     throw new BusinessException ("Name 已存在");
    //   }
    //   cardcollection.Name = tarotCardCollectionUpdateDto.Name;
    //   cardcollection.Descript = tarotCardCollectionUpdateDto.Descript;
    //   cardcollection.CardBackImgUrl = tarotCardCollectionUpdateDto.CardBackImgUrl;
    //   cardcollection.CardFrontImgUrl = tarotCardCollectionUpdateDto.CardFrontImgUrl;
    //   var collection = await cardCollectionRepository.UpdateAsync (cardcollection);
    //   return ObjectMapper.Map<TarotCardCollection, TarotCardCollectionDto> (collection);
    // }

    public async Task<TarotCardCollectionDto> AddCard (Guid id, List<Guid> ids) {
      var cardcollection = await cardCollectionRepository.GetAsync (x => x.Id == id);
      Check.NotNull (cardcollection, nameof (cardcollection));
      await cardCollectionRepository.EnsureCollectionLoadedAsync (cardcollection, c => c.TarotCards);

      var cards = await tarotCardRepository.Where (x => ids.Contains (x.Id)).ToListAsync ();
      // TarotCard card = new TarotCard (tarotCardDto.Id, tarotCardDto.Name, tarotCardDto.Descript, tarotCardDto.CardFrontImgUrl, tarotCardDto.CardContentText);
      // cardcollection.AddCard (card);
      cards.ForEach (card => {
        cardcollection.AddCard (card);
      });
      var collection = await cardCollectionRepository.UpdateAsync (cardcollection);
      return ObjectMapper.Map<TarotCardCollection, TarotCardCollectionDto> (collection);
    }

    public async Task<TarotCardCollectionDto> RemoveCard (Guid id, List<Guid> ids) {
      var cardCollection = await cardCollectionRepository.GetAsync (x => x.Id == id);
      Check.NotNull (cardCollection, nameof (cardCollection));
      await cardCollectionRepository.EnsureCollectionLoadedAsync (cardCollection, c => c.TarotCards);
      var cards = await tarotCardRepository.Where (x => ids.Contains (x.Id)).ToListAsync ();
      cards.ForEach (card => {
        cardCollection.RemoveCard (card);
      });
      var collection = await cardCollectionRepository.UpdateAsync (cardCollection);
      return ObjectMapper.Map<TarotCardCollection, TarotCardCollectionDto> (collection);
    }

    #region 
    /**
        public async Task<TarotCardCollectionDto> FindByIdAsync (Guid id) {

          #region 使用仓储
          var queryable = cardCollectionRepository
            .Where (x => x.Id == id)
            .Include (t => t.TarotCards);
          var collections = await AsyncExecuter.FirstOrDefaultAsync (queryable);
          #endregion

          #region 使用 GetQueryableAsync
          // var queryable = await cardCollectionRepository.GetQueryableAsync ();
          // var result = queryable
          // .Where (x => x.Id == id)
          // .Include (t => t.TarotCards);
          // var collections = await AsyncExecuter.FirstOrDefaultAsync (result);
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
        **/
    #endregion
    // public async Task<List<TarotCardCollectionDto>> GetListAsync () {
    //   var collections = await manager.GetListAsync ();
    //   return ObjectMapper.Map<List<TarotCardCollection>, List<TarotCardCollectionDto>> (collections);
    // }

    // public async List<TarotCardCollection> FindByNameWidthDetailsAsync (Guid tarotCardCollectionId) {
    //   var cardcollection = await cardCollectionRepository.GetAsync (c => c.Id == tarotCardCollectionId);
    //   var cardcollection = await cardCollectionRepository.GetAsync (x => x.Id == tarotCardCollectionId);
    //   return cardcollection;
    // }
  }
}