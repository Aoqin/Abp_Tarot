using Acme.Tarot.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.Tarot.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class TarotController : AbpController
    {
        protected TarotController()
        {
            LocalizationResource = typeof(TarotResource);
        }
    }
}