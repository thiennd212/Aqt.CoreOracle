using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Aqt.CoreOracle.Pages;

[Collection(CoreOracleTestConsts.CollectionDefinitionName)]
public class Index_Tests : CoreOracleWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
