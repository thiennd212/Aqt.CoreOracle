namespace Aqt.CoreOracle;

public static class CoreOracleDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */
    public const string CategoryTypeCodeAlreadyExists = "CoreOracle:CategoryType:001";
    public const string CategoryTypeCodeRequired = "CoreOracle:CategoryType:002";
    public const string CategoryTypeNameRequired = "CoreOracle:CategoryType:003";
    public const string CategoryTypeNotFound = "CoreOracle:Category:002";
    public const string CategoryItemCodeAlreadyExists = "CoreOracle:CategoryItem:001";
    public const string CategoryItemCodeRequired = "CoreOracle:CategoryItem:002";
    public const string CategoryItemNameRequired = "CoreOracle:CategoryItem:003";
    public const string InvalidParentCategory = "CoreOracle:Category:004";
    public const string CategoryItemParentTypeMismatch = "CoreOracle:CategoryItem:004";
    public const string CategoryItemTypeMismatch = "CoreOracle:CategoryItem:005";
    public const string PositionCodeAlreadyExists = "CoreOracle:00001";
    public const string UserAlreadyAssignedToPositionInOU = "CoreOracle:00002";
    public const string PositionNotFound = "CoreOracle:00003";
    public const string OrganizationUnitNotFound = "CoreOracle:00004";
    public const string InvalidPositionAssignmentDateRange = "CoreOracle:00005";
}
