using System.Linq;
using Acme.Tarot.Cards;
using Microsoft.EntityFrameworkCore;

namespace Acme.Tarot.EntityFrameworkCore {
  public static class TarotManagementEfCoreQueryableExtensions {
    // public static IQueryable<TarotCardCollection> IncludeDetails (this IQueryable<TarotCardCollection> queryable, bool include = true) {
    //   if (!include) {
    //     return queryable;
    //   }

    //   return queryable
    //     .Include (x => x.CardBindCollections)
    //     .ThenInclude (m => m.TarotCard);
    // }
  }
}
