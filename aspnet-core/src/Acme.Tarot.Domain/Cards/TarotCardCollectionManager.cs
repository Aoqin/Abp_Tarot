using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Acme.Tarot.Cards {
  public class TarotCardCollectionManager : DomainService {
    public readonly IRepository<TarotCardCollection, Guid> _repository;
    public TarotCardCollectionManager (IRepository<TarotCardCollection, Guid> repository) {
      _repository = repository;
    }

    public async Task<List<TarotCardCollection>> GetListAsync () {
      var queryable = await _repository.WithDetailsAsync (x => x.TarotCards);
      var collections = await AsyncExecuter.ToListAsync (queryable);
      return collections;
    }

    // public async Task<TarotCardCollection> AddCards (TarotCardCollection tarotCardCollection, TarotCard tarotCard) {
    //   var collection = await _repository.GetAsync ()
    // }
  }
}