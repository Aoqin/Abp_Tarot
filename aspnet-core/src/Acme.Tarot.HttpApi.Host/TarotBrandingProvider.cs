using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Acme.Tarot
{
    [Dependency(ReplaceServices = true)]
    public class TarotBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Tarot";
    }
}
