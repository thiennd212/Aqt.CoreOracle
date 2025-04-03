using Volo.Abp.Reflection;

namespace Aqt.CoreOracle.[ModuleName]
{
    /// <summary>
    /// Defines permissions for [EntityName]s
    /// </summary>
    public static class [ModuleName]Permissions
    {
        public const string GroupName = "CoreOracle";

        public static class [EntityName]s
        {
            public const string Default = GroupName + ".[EntityName]s";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof([ModuleName]Permissions));
        }
    }

    /// <summary>
    /// Defines permission definitions for [EntityName]s
    /// </summary>
    public class [ModuleName]PermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var coreOracleGroup = context.GetGroup([ModuleName]Permissions.GroupName);

            var [entityName]sPermission = coreOracleGroup.AddPermission(
                [ModuleName]Permissions.[EntityName]s.Default,
                L("Permission:[EntityName]s"));

            [entityName]sPermission.AddChild(
                [ModuleName]Permissions.[EntityName]s.Create,
                L("Permission:[EntityName]s.Create"));

            [entityName]sPermission.AddChild(
                [ModuleName]Permissions.[EntityName]s.Edit,
                L("Permission:[EntityName]s.Edit"));

            [entityName]sPermission.AddChild(
                [ModuleName]Permissions.[EntityName]s.Delete,
                L("Permission:[EntityName]s.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CoreOracleResource>(name);
        }
    }
} 