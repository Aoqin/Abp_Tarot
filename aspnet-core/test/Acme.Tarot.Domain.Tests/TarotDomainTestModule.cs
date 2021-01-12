using Acme.Tarot.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Acme.Tarot
{
    [DependsOn(
        typeof(TarotEntityFrameworkCoreTestModule)
        )]
    public class TarotDomainTestModule : AbpModule
    {

    }
}