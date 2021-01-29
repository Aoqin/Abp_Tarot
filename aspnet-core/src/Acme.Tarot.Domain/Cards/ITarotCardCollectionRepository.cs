using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.Tarot.Cards {
  public interface ITarotCardCollectionRepository : IRepository<TarotCardCollection, Guid> {
    Task<List<TarotCardCollection>> FindByNameAsync (string name);
  }
}