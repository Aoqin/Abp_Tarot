using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Tarot.Cards {
  public class TarotCardCollection : AuditedAggregateRoot<Guid> {
    public string Name { get; set; }
    public string Descript { get; set; }
    public string CardFrontImgUrl { get; set; }
    public string CardBackImgUrl { get; set; }
    public ICollection<TarotCard> TarotCards { get; set; }
    public List<CardBindCollection> CardBindCollections { get; set; }
  }
}