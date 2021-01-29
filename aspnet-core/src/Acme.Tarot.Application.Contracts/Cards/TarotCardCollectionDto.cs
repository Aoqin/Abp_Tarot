using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Acme.Tarot.Cards {
  public class TarotCardCollectionDto : AuditedEntityDto<Guid> {
    public string Name { get; set; }
    public string Descript { get; set; }
    public string CardFrontImgUrl { get; set; }
    public string CardBackImgUrl { get; set; }
    public ICollection<TarotCardDto> TarotCards { get; set; }
  }
}