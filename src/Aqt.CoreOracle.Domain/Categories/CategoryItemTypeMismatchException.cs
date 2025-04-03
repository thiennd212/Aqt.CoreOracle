using Volo.Abp;

namespace Aqt.CoreOracle.Categories;

public class CategoryItemTypeMismatchException : BusinessException
{
    public CategoryItemTypeMismatchException()
        : base(CoreOracleDomainErrorCodes.CategoryItemTypeMismatch)
    {
    }
} 