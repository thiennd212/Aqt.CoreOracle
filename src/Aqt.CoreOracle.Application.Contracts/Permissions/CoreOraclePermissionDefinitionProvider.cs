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

        var organizationManagementPermission = coreOracleGroup.AddPermission(
            CoreOraclePermissions.OrganizationManagement.Positions.Default,
            L("Permission:OrganizationManagement.Positions"));

        organizationManagementPermission.AddChild(
            CoreOraclePermissions.OrganizationManagement.Positions.Create,
            L("Permission:OrganizationManagement.Positions.Create"));

        organizationManagementPermission.AddChild(
            CoreOraclePermissions.OrganizationManagement.Positions.Update,
            L("Permission:OrganizationManagement.Positions.Update"));

        organizationManagementPermission.AddChild(
            CoreOraclePermissions.OrganizationManagement.Positions.Delete,
            L("Permission:OrganizationManagement.Positions.Delete"));

        coreOracleGroup.AddPermission(
            CoreOraclePermissions.OrganizationManagement.AssignPositions,
            L("Permission:OrganizationManagement.AssignPositions"));

        // Add definitions for custom Organization Unit permissions
        var ouPermission = coreOracleGroup.AddPermission(
            CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default,
            L("Permission:OrganizationManagement.OrganizationUnits"));
        ouPermission.AddChild(
            CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Create,
            L("Permission:OrganizationManagement.OrganizationUnits.Create"));
        ouPermission.AddChild(
            CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Update,
            L("Permission:OrganizationManagement.OrganizationUnits.Update"));
        ouPermission.AddChild(
            CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Delete,
            L("Permission:OrganizationManagement.OrganizationUnits.Delete"));
        ouPermission.AddChild(
            CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Move,
            L("Permission:OrganizationManagement.OrganizationUnits.Move"));

        var countriesPermission = coreOracleGroup.AddPermission(CoreOraclePermissions.Countries.Default, L("Permission:Countries"));
        countriesPermission.AddChild(CoreOraclePermissions.Countries.Create, L("Permission:Create"));
        countriesPermission.AddChild(CoreOraclePermissions.Countries.Edit, L("Permission:Edit"));
        countriesPermission.AddChild(CoreOraclePermissions.Countries.Delete, L("Permission:Delete"));

        var provincesPermission = coreOracleGroup.AddPermission(
            CoreOraclePermissions.Provinces.Default,
            L("Permission:Provinces"));

        provincesPermission.AddChild(
            CoreOraclePermissions.Provinces.Create,
            L("Permission:Provinces.Create"));

        provincesPermission.AddChild(
            CoreOraclePermissions.Provinces.Edit,
            L("Permission:Provinces.Edit"));

        provincesPermission.AddChild(
            CoreOraclePermissions.Provinces.Delete,
            L("Permission:Provinces.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CoreOracleResource>(name);
    }
}
