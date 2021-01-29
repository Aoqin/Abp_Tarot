using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Tarot.Cards {
  public class TarotCardCollection : AuditedAggregateRoot<Guid> {
    public string Name { get; set; }
    public string Descript { get; set; }
    public string CardFrontImgUrl { get; set; }
    public string CardBackImgUrl { get; set; }
    public ICollection<TarotCard> TarotCards { get; set; }
    public List<CardBindCollection> CardBindCollections { get; set; }

    public TarotCardCollection () {
      TarotCards = new Collection<TarotCard> ();
    }

    internal TarotCardCollection (
      Guid id, [NotNull] string name,
      string descript,
      string cardFrontImgUrl,
      string cardBackImgUrl
    ) : base (id) {
      Name = name;
      Descript = descript;
      CardFrontImgUrl = cardFrontImgUrl;
      CardBackImgUrl = CardBackImgUrl;
    }

    public void AddCard (TarotCard tarotCard) {
      if (TarotCards.Contains (tarotCard)) {
        throw new ArgumentException ($"tarotcard {tarotCard.Name} is added");
      }
      TarotCards.Add (tarotCard);
    }
  }
}