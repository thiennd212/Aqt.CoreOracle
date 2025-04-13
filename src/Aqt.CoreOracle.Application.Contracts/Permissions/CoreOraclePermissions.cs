namespace Aqt.CoreOracle.Permissions;

public static class CoreOraclePermissions
{
    public const string GroupName = "CoreOracle";

    public static class OrganizationManagement
    {        
        public const string AssignPositions = GroupName + ".AssignPositions";


        public static class Positions
        {
            public const string Default = GroupName + ".Positions";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class OrganizationUnits
        {
            public const string Default = GroupName + ".OrganizationUnits";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Move = Default + ".Move";
        }
    }

    public static class Countries
    {
        public const string Default = GroupName + ".Countries";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Provinces
    {
        public const string Default = GroupName + ".Provinces";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
