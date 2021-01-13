using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Tarot.Cards {
    public class TarotCard : AuditedAggregateRoot<Guid> {
        public string Name { get; set; }
        public string Descript { get; set; }
        public string CardFrontImgUrl { get; set; }
        public string CardContentText { get; set; }
    }
}