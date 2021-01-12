using Acme.Tarot.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.Tarot.Permissions
{
    public class TarotPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(TarotPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(TarotPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TarotResource>(name);
        }
    }
}
