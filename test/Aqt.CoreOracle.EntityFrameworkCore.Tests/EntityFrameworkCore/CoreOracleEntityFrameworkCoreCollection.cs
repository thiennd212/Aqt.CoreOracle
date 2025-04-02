using Xunit;

namespace Aqt.CoreOracle.EntityFrameworkCore;

[CollectionDefinition(CoreOracleTestConsts.CollectionDefinitionName)]
public class CoreOracleEntityFrameworkCoreCollection : ICollectionFixture<CoreOracleEntityFrameworkCoreFixture>
{

}
