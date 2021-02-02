using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Tarot.Cards {
    public class TarotCard : AuditedAggregateRoot<Guid> {
        public string Name { get; set; }
        public string Descript { get; set; }
        public string CardFrontImgUrl { get; set; }
        public string CardContentText { get; set; }
        public ICollection<TarotCardCollection> TarotCardCollections { get; set; }
        public List<CardBindCollection> CardBindCollections;

        // public TarotCard (
        //     Guid id, [NotNull] string name,
        //     string descript,
        //     string cardFrontImgUrl,
        //     string cardContentText
        // ) : base (id) {
        //     Check.NotNull (id, nameof (id));
        //     Name = name;
        //     Descript = descript;
        //     CardFrontImgUrl = cardFrontImgUrl;
        //     CardContentText = cardContentText;
        // }
    }
}