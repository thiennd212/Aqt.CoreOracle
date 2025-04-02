using Aqt.CoreOracle.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Aqt.CoreOracle.Permissions;

public class CoreOraclePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CoreOraclePermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(CoreOraclePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CoreOracleResource>(name);
    }
}
