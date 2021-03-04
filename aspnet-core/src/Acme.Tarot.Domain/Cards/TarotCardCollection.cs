using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Tarot.Cards {
  public class TarotCardCollection : AuditedAggregateRoot<Guid> {
    public string Name { get; set; }
    public string Descript { get; set; }
    public string CardFrontImgUrl { get; set; }
    public string CardBackImgUrl { get; set; }
    public ICollection<TarotCard> TarotCards { get; set; }
    public List<CardBindCollection> CardBindCollections { get; set; }
    public int DivinationLimit { get; set; }

    public TarotCardCollection () {
      TarotCards = new Collection<TarotCard> ();
    }

    internal TarotCardCollection (
      Guid id, [NotNull] string name,
      string descript,
      string cardFrontImgUrl,
      string cardBackImgUrl,
      int divinationLimit
    ) : base (id) {
      Name = name;
      Descript = descript;
      CardFrontImgUrl = cardFrontImgUrl;
      CardBackImgUrl = CardBackImgUrl;
      DivinationLimit = divinationLimit;
    }

    public virtual void AddCard (TarotCard tarotCard) {
      if (IsInCollection (tarotCard.Id)) {
        return;
      }
      TarotCards.Add (tarotCard);
    }

    public virtual void RemoveCard (TarotCard tarotCard) {
      if (!IsInCollection (tarotCard.Id)) {
        return;
      }
      TarotCards.Remove (tarotCard);
    }

    public virtual bool IsInCollection (Guid id) {
      Check.NotNull (id, nameof (id));
      return TarotCards.Any (c => c.Id == id);
    }
  }
}