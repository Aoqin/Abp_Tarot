using System;
using Volo.Abp.Domain.Entities;

namespace Acme.Tarot.Domain.Cards {
    public class TarotCard : Entity<Guid> {
        public string Name { get; set; }
        public string Descript { get; set; }
        public string CardFrontImgUrl { get; set; }
        public string CardContentText { get; set; }
    }
}