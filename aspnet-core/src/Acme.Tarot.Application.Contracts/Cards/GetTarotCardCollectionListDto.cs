using Volo.Abp.Application.Dtos;

namespace Acme.Tarot.Cards {
  public class GetTarotCardCollectionListDto : PagedAndSortedResultRequestDto {
    public string Filter { get; set; }
  }
}