namespace Aqt.CoreOracle.Permissions;

public static class CoreOraclePermissions
{
    public const string GroupName = "CoreOracle";

    public static class CategoryTypes
    {
        public const string Default = GroupName + ".CategoryTypes";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class CategoryItems
    {
        public const string Default = GroupName + ".CategoryItems";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
