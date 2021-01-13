using System;
using Volo.Abp.Application.Dtos;

namespace Acme.Tarot.Cards {
    public class TarotCardDto : AuditedEntityDto<Guid> {
        public string Name { get; set; }
        public string Descript { get; set; }
        public string CardFrontImgUrl { get; set; }
        public string CardContentText { get; set; }
    }
}