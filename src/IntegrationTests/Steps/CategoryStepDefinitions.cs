using System.Net.Http.Json;
using Application.Commands.Categories;
using IntegrationTests.Seeds;
using IntegrationTests.TestsSetup;
using Reqnroll;

[Binding]
public class CategoryStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    public CategoryStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }
    
    [When(@"the user sends a POST request to \/api\/categories with the following data")]
    public async Task GivenTheUserSendsAPOSTRequestToApiCategoriesWithTheFollowingData(Table table)
    {
        var command = table.CreateInstance<AddCategoryCommand>();
        var integrationTestsContext = _scenarioContext.Get<IntegrationTestsContext>(ScenarioContextKeys.IntegrationTestsContext);
        
        var response = await integrationTestsContext.ApiClient.PostAsJsonAsync("/api/categories", command);
        _scenarioContext.Set(response, ScenarioContextKeys.ApiResponse);
    }
}