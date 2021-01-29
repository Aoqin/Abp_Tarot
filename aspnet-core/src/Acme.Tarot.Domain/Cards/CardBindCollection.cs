using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Tarot.Cards {
  public class CardBindCollection : AuditedEntity {
    public virtual Guid TarotCardId { get; set; }
    public virtual TarotCard TarotCard { get; set; }
    public virtual Guid TarotCardCollecitonId { get; set; }
    public virtual TarotCardCollection TarotCardCollection { get; set; }

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