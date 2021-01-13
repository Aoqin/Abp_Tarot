using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Acme.Tarot.Cards {
  public class TarotCardCreateDto : AuditedEntityDto<Guid> {
    [Required]
    [StringLength (32)]
    public string Name { get; set; }

    [Required]
    [MaxLength (200)]
    public string Descript { get; set; }

    // [RegularExpression('')]
    [MaxLength (256)]
    public string CardFrontImgUrl { get; set; }

    [MaxLength (32)]
    public string CardContentText { get; set; }
  }
}