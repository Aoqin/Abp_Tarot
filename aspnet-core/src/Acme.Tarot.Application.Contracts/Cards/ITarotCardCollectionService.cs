using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.Tarot.Cards {
  public interface ITarotCardCollectionService : IApplicationService
  //  ICrudAppService<TarotCardCollectionDto, Guid, PagedAndSortedResultRequestDto, TarotCardCollectionCreateDto, TarotCardCollectionUpdateDto>
  {
    Task<TarotCardCollectionDto> GetAsync (Guid id);

    Task<PagedResultDto<TarotCardCollectionDto>> GetListAsync (GetTarotCardCollectionListDto input);
  }
}