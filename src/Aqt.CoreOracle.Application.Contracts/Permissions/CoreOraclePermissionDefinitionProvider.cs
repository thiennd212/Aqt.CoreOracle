using Aqt.CoreOracle.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Aqt.CoreOracle.Permissions;

public class CoreOraclePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var coreOracleGroup = context.AddGroup(CoreOraclePermissions.GroupName, L("Permission:CoreOracle"));

        var categoryTypesPermission = coreOracleGroup.AddPermission(CoreOraclePermissions.CategoryTypes.Default, L("Permission:CategoryTypes"));
        categoryTypesPermission.AddChild(CoreOraclePermissions.CategoryTypes.Create, L("Permission:CategoryTypes.Create"));
        categoryTypesPermission.AddChild(CoreOraclePermissions.CategoryTypes.Edit, L("Permission:CategoryTypes.Edit"));
        categoryTypesPermission.AddChild(CoreOraclePermissions.CategoryTypes.Delete, L("Permission:CategoryTypes.Delete"));

        var categoryItemsPermission = coreOracleGroup.AddPermission(CoreOraclePermissions.CategoryItems.Default, L("Permission:CategoryItems"));
        categoryItemsPermission.AddChild(CoreOraclePermissions.CategoryItems.Create, L("Permission:CategoryItems.Create"));
        categoryItemsPermission.AddChild(CoreOraclePermissions.CategoryItems.Edit, L("Permission:CategoryItems.Edit"));
        categoryItemsPermission.AddChild(CoreOraclePermissions.CategoryItems.Delete, L("Permission:CategoryItems.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CoreOracleResource>(name);
    }
}
