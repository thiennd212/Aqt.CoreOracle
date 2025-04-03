using System.Threading.Tasks;
using Aqt.CoreOracle.Localization;
using Aqt.CoreOracle.Permissions;
using Aqt.CoreOracle.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;

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

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<CoreOracleResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                CoreOracleMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        //Categories
        var categoriesMenu = new ApplicationMenuItem(
            "Categories",
            l["Menu:Categories"],
            icon: "fa fa-list",
            order: 2
        );

        categoriesMenu.AddItem(
            new ApplicationMenuItem(
                "CategoryTypes",
                l["Menu:CategoryTypes"],
                url: "/Categories/CategoryTypes",
                icon: "fa fa-folder",
                requiredPermissionName: CoreOraclePermissions.CategoryTypes.Default
            )
        );

        categoriesMenu.AddItem(
            new ApplicationMenuItem(
                "CategoryItems",
                l["Menu:CategoryItems"],
                url: "/Categories/CategoryItems",
                icon: "fa fa-list-alt",
                requiredPermissionName: CoreOraclePermissions.CategoryItems.Default
            )
        );

        context.Menu.AddItem(categoriesMenu);

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 6;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);
    
        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }
        
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);
        
        return Task.CompletedTask;
    }
}
