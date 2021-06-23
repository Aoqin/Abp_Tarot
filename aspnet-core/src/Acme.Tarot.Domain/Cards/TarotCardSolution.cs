using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Tarot.Cards
{
  public class TarotCardSolution : AuditedEntity
  {

    public Guid TarotCardCollectionId { get; set; }
    public List<Guid> TarotCardIds { get; set; }
    public virtual List<TarotCard> TarotCards { get; set; } = new List<TarotCard>();
    public string Hexagram { get; set; }
    public string HexagramExplain { get; set; }

    public TarotCardSolution() { }

    internal TarotCardSolution([NotNull] Guid tarotCardCollectionId, [NotNull] List<Guid> tarotCardIds, string hexagram, string hexagramExplain)
    {
      TarotCardCollectionId = tarotCardCollectionId;
      TarotCardIds = tarotCardIds;
      Hexagram = hexagram;
      HexagramExplain = hexagramExplain;
    }

    public void AddCards(TarotCard card)
    {
      Check.NotNull(card, nameof(card));
      if (IsInCollection(card.Id)) return;

      TarotCards.Add(card);
    }

    public virtual bool IsInCollection(Guid id)
    {
      Check.NotNull(id, nameof(id));
      return TarotCards.Exists(i => i.Id == id);
    }
    public override Object[] GetKeys()
    {
      // TarotCardIds.Sort ();
      // TarotCards
      // string tarotCardCollectionIdString = TarotCardCollectionId.ToString ().ToUpper ();
      // string TarotCardIdsToString = string.Join (",", TarotCards.ToArray (x=>x.TarotCardId));

      return new Object[] { TarotCardIds };
    }
  }
}