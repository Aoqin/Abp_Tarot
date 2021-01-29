using Acme.Tarot.Cards;
using AutoMapper;

namespace Acme.Tarot {
    public class TarotApplicationAutoMapperProfile : Profile {
        public TarotApplicationAutoMapperProfile () {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<TarotCard, TarotCardDto> ();
            CreateMap<TarotCardCreateDto, TarotCard> ();
            CreateMap<TarotCardUpdateDto, TarotCard> ();

            CreateMap<TarotCardCollection, TarotCardCollectionDto> ();
            CreateMap<TarotCardCollectionCreateDto, TarotCardCollection> ();
            CreateMap<TarotCardCollectionUpdateDto, TarotCardCollection> ();
        }
    }
}