using System.Threading.Tasks;
using Aqt.CoreOracle.Localization;
using Aqt.CoreOracle.Permissions;
using Aqt.CoreOracle.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Aqt.CoreOracle.Application;
using Volo.Abp.Identity.Localization;
using System.Linq;

namespace Aqt.CoreOracle.Web.Menus;

public class CoreOracleMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<CoreOracleResource>();

        context.Menu.Items.Insert(0, new ApplicationMenuItem(
            CoreOracleMenus.Home,
            l["Menu:Home"],
            "~/",
            icon: "fas fa-home",
            order: 0
        ));

        // Add Countries menu item if user has default permission
        if (await context.IsGrantedAsync(CoreOraclePermissions.Countries.Default))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                CoreOracleMenus.Countries,
                l["Menu:Countries"],
                "/Countries",
                icon: "fas fa-globe",
                order: 1
            ).RequirePermissions(CoreOraclePermissions.Countries.Default));
        }

        // Add Province/City Management menu item if user has default permission
        if (await context.IsGrantedAsync(CoreOraclePermissions.Provinces.Default))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                CoreOracleMenus.Provinces,
                l["Menu:Provinces"],
                "/Provinces",
                icon: "fas fa-city",
                order: 2
            ).RequirePermissions(CoreOraclePermissions.Provinces.Default));
        }

        // Add Organization Management Submenu if user has permissions
        if (await context.IsGrantedAsync(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default) || // Or has default OU permission
            await context.IsGrantedAsync(CoreOraclePermissions.OrganizationManagement.Positions.Default)) // Or has position permission
        {
            var organizationManagementMenuItem = new ApplicationMenuItem(
                CoreOracleMenus.OrganizationManagement.GroupName,
                l["Menu:OrganizationManagement"],
                icon: "fas fa-sitemap",
                order: 3 // Place it after Countries and Province/City
            );
            context.Menu.AddItem(organizationManagementMenuItem);

            // Add Positions menu item if user has permission
            if (await context.IsGrantedAsync(CoreOraclePermissions.OrganizationManagement.Positions.Default))
            {
                organizationManagementMenuItem.AddItem(new ApplicationMenuItem(
                    CoreOracleMenus.OrganizationManagement.Positions,
                    l["Menu:Positions"],
                    url: "/Positions",
                    icon: "fas fa-id-badge",
                    requiredPermissionName: CoreOraclePermissions.OrganizationManagement.Positions.Default
                ));
            }

            // Add Organization Structure menu item if user has the correct permission
            if (await context.IsGrantedAsync(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default))
            {
                organizationManagementMenuItem.AddItem(new ApplicationMenuItem(
                    CoreOracleMenus.OrganizationManagement.OrganizationStructure, // Assuming this constant exists
                    l["Menu:OrganizationStructure"],
                    url: "/OrganizationStructure", // Points to the new page
                    icon: "fas fa-network-wired",
                    requiredPermissionName: CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default // Use correct permission
                ));
            }
        }

        if (administration != null)
        {
            // Move Tenant Management if needed
            var tenantManagementMenuItem = administration.GetMenuItem(TenantManagementMenuNames.GroupName);
            if (tenantManagementMenuItem != null)
            {
                administration.Items.Remove(tenantManagementMenuItem);
                // Optionally add it back to the main menu or a different group
                // context.Menu.AddItem(tenantManagementMenuItem);
            }

            // Identity Menu Item
            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 4);

            // Settings Menu Item
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 5);
        }
    }
}
