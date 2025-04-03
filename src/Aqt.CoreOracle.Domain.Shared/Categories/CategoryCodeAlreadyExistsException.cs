using Volo.Abp;

namespace Aqt.CoreOracle.Categories;

public class CategoryCodeAlreadyExistsException : BusinessException
{
    public CategoryCodeAlreadyExistsException(string code)
        : base(CoreOracleDomainErrorCodes.CategoryCodeAlreadyExists)
    {
        WithData("code", code);
    }
} 