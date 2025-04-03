using Volo.Abp;

namespace Aqt.CoreOracle.Categories;

public class CategoryItemParentTypeMismatchException : BusinessException
{
    public CategoryItemParentTypeMismatchException()
        : base(CoreOracleDomainErrorCodes.CategoryItemParentTypeMismatch)
    {
    }
} 