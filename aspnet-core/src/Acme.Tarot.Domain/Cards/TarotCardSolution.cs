using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Tarot.Cards {
  public class TarotCardSolution : AuditedEntity {

    public virtual Guid TarotCardCollectionId { get; set; }
    public virtual string TarotCardIdsToString { get; set; }
    public virtual string Hexagram { get; set; }

    public virtual string HexagramExplain { get; set; }

    public TarotCardSolution () {
    }

    internal TarotCardSolution (Guid tarotCardCollectionId, List<Guid> tarotCardIds, string hexagram, string hexagramExplain) {
      TarotCardCollectionId = tarotCardCollectionId;
      tarotCardIds.Sort ();
      TarotCardIdsToString = string.Join (",", tarotCardIds.ToArray ());
      Hexagram = hexagram;
      HexagramExplain = hexagramExplain;
    }

    public override Object[] GetKeys () {
      // string tarotCardCollectionIdString = TarotCardCollectionId.ToString ().ToUpper ();
      return new Object[] { TarotCardCollectionId, TarotCardIdsToString };
    }
  }
}