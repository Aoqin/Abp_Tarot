using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Tarot.Cards {
  public class CardBindCollection : AuditedEntity {
    public Guid TarotCardId { get; set; }
    public TarotCard TarotCard { get; set; }
    public Guid TarotCardCollecitonId { get; set; }
    public TarotCardCollection TarotCardCollection { get; set; }

    protected CardBindCollection () { }

    internal CardBindCollection (Guid tarotCardId, Guid tarotCardCollectionId) {
      TarotCardId = tarotCardId;
      TarotCardCollecitonId = tarotCardCollectionId;
    }
    public override Object[] GetKeys () {
      return new Object[] { TarotCardId, TarotCardCollecitonId };
    }
  }
}