using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.Tarot.Cards {
  public interface ITarotCardService : ICrudAppService<TarotCardDto, Guid, PagedAndSortedResultRequestDto, TarotCardCreateDto, TarotCardUpdateDto> { }
}