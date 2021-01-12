using Volo.Abp.Modularity;

namespace Acme.Tarot
{
    [DependsOn(
        typeof(TarotApplicationModule),
        typeof(TarotDomainTestModule)
        )]
    public class TarotApplicationTestModule : AbpModule
    {

    }
}