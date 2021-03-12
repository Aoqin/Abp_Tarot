using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Tarot.Cards {
  public class TarotCardSolutionDto : AuditedEntityDto {

    public virtual Guid TarotCardCollectionId { get; set; }
    public virtual TarotCardCollectionDto TarotCardCollection { get; set; }
    public virtual List<TarotCardDto> TarotCards { get; set; }
    public virtual string Hexagram { get; set; }
    public virtual string HexagramExplain { get; set; }

  }
}