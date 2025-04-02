using Microsoft.AspNetCore.Builder;
using Aqt.CoreOracle;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Aqt.CoreOracle.Web.csproj"); 
await builder.RunAbpModuleAsync<CoreOracleWebTestModule>(applicationName: "Aqt.CoreOracle.Web");

public partial class Program
{
}
