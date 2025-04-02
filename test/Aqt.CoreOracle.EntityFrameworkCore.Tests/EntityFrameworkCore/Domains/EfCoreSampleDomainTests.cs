using Aqt.CoreOracle.Samples;
using Xunit;

namespace Aqt.CoreOracle.EntityFrameworkCore.Domains;

[Collection(CoreOracleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<CoreOracleEntityFrameworkCoreTestModule>
{

}
