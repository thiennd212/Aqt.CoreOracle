using Volo.Abp;

namespace Aqt.CoreOracle.Categories;

public class CategoryItemCodeAlreadyExistsException : BusinessException
{
    public CategoryItemCodeAlreadyExistsException(string code)
        : base(CoreOracleDomainErrorCodes.CategoryItemCodeAlreadyExists)
    {
        WithData("code", code);
    }
} 