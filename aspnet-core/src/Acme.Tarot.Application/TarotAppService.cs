using System;
using System.Collections.Generic;
using System.Text;
using Acme.Tarot.Localization;
using Volo.Abp.Application.Services;

namespace Acme.Tarot
{
    /* Inherit your application services from this class.
     */
    public abstract class TarotAppService : ApplicationService
    {
        protected TarotAppService()
        {
            LocalizationResource = typeof(TarotResource);
        }
    }
}
