using Aqt.CoreOracle.Samples;
using Xunit;

namespace Aqt.CoreOracle.EntityFrameworkCore.Applications;

[Collection(CoreOracleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<CoreOracleEntityFrameworkCoreTestModule>
{

}
