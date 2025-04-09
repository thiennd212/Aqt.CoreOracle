using Aqt.CoreOracle.Localization;
using Aqt.CoreOracle.Domain.Shared.OrganizationUnits;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.Threading;

namespace Aqt.CoreOracle.Domain.Shared;

public static class CoreOracleModuleExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ConfigureExistingProperties();
            ConfigureExtraProperties();
        });
    }

    private static void ConfigureExistingProperties()
    {
        /* You can change max lengths for properties of the
         * entities defined in the modules used by your application.
         *
         * Example: Change user and role name max lengths

           AbpUserConsts.MaxNameLength = 99;
           IdentityRoleConsts.MaxNameLength = 99;

         * Notice: It is not suggested to change property lengths
         * unless you really need it. Go with the standard values wherever possible.
         *
         * If you are using EF Core, you will need to run the add-migration command after your changes.
         */
    }

    private static void ConfigureExtraProperties()
    {
        ObjectExtensionManager.Instance.Modules()
            .ConfigureIdentity(identity =>
            {
                identity.ConfigureOrganizationUnit(ou =>
                {
                    ou.AddOrUpdateProperty<string>(
                        CoreOracleConsts.OrganizationUnitAddress,
                        property =>
                        {
                            property.Attributes.Add(
                                new MaxLengthAttribute(
                                    CoreOracleConsts.MaxOrganizationUnitAddressLength
                                )
                            );
                            property.DisplayName = L("OrganizationUnit:Address");
                        }
                    );

                    ou.AddOrUpdateProperty<string>(
                        CoreOracleConsts.OrganizationUnitSyncCode,
                        property =>
                        {
                            property.Attributes.Add(
                                new MaxLengthAttribute(
                                    CoreOracleConsts.MaxOrganizationUnitSyncCodeLength
                                )
                            );
                            property.DisplayName = L("OrganizationUnit:SyncCode");
                        }
                    );

                    ou.AddOrUpdateProperty<OrganizationUnitTypeEnum>(
                        CoreOracleConsts.OrganizationUnitType,
                        property =>
                        {
                            property.DefaultValue = OrganizationUnitTypeEnum.Unit;
                            property.DisplayName = L("OrganizationUnit:OrganizationUnitType");
                        }
                    );
                });
            });
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CoreOracleResource>(name);
    }
}
