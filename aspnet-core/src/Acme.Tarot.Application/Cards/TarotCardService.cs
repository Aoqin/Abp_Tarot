using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.Tarot.Cards {
  public class TarotCardService : CrudAppService<TarotCard, TarotCardDto, Guid, PagedAndSortedResultRequestDto, TarotCardCreateDto, TarotCardUpdateDto>, ITarotCardService {
    public TarotCardService (IRepository<TarotCard, Guid> repository) : base (repository) {

    }
  }
}