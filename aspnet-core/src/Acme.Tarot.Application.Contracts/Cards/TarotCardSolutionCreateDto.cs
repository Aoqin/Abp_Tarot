using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Acme.Tarot.Cards {
  public class TarotCardSolutionCreateDto : AuditedEntityDto {

    [Required]
    public Guid TarotCardCollectionId { get; set; }

    public List<Guid> TarotCardIds { get; set; }
    public string Hexagram { get; set; }

    [Required]
    public string HexagramExplain { get; set; }

  }
}