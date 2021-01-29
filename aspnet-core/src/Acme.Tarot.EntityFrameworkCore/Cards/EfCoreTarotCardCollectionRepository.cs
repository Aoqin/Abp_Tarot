using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.Tarot.Cards;
using Acme.Tarot.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.Tarot.Cards {
  public class EfCoreTarotCardCollectionRepository
    : EfCoreRepository<TarotDbContext, TarotCardCollection, Guid>,
    ITarotCardCollectionRepository {
      public EfCoreTarotCardCollectionRepository (
        IDbContextProvider<TarotDbContext> dbContextProvider) : base (dbContextProvider) { }

      public async Task<List<TarotCardCollection>> FindByNameAsync (string name) {
        var dbSet = await GetDbSetAsync ();
        return await dbSet
          .Where (c => c.Name == name)
          .Include(c => c.TarotCards)
          .ToListAsync ();
      }

      // public async Task<List<TarotCardCollection>> GetListAsync (
      //   int skipCount,
      //   int maxResultCount,
      //   string sorting,
      //   string filter = null) {
      //   var dbSet = await GetDbSetAsync ();
      //   return await dbSet
      //     .WhereIf (!filter.IsNullOrWhiteSpace (),
      //       TarotCardCollection => TarotCardCollection.Name.Contains (filter)
      //     )
      //     .OrderBy (sorting)
      //     .Skip (skipCount)
      //     .Take (maxResultCount)
      //     .ToListAsync ();
      // }
    }
}