namespace Aqt.CoreOracle.Web.Menus;

public class CoreOracleMenus
{
    private const string Prefix = "CoreOracle";

    public const string Home = Prefix + ".Home";
    public const string Countries = Prefix + ".Countries";
    public const string Provinces = Prefix + ".Provinces";

    //Add main menu items here
    public static class OrganizationManagement
    {
        public const string GroupName = Prefix + ".OrganizationManagement";
        public const string Positions = GroupName + ".Positions";
        public const string OrganizationStructure = GroupName + ".OrganizationStructure";
    }
}
