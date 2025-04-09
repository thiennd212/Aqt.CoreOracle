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

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
