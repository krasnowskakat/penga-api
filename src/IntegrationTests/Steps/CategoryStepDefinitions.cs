using System.Net.Http.Json;
using Application.Commands.Categories;
using Application.Models;
using FluentAssertions;
using IntegrationTests.Seeds;
using IntegrationTests.TestsSetup;
using Microsoft.EntityFrameworkCore;
using Reqnroll;

[Binding]
public class CategoryStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    public CategoryStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }
    
    [Given("category with the name (.*) exists in the database")]
    public async Task GivenCategoryExistsInTheDatabase(string categoryName)
    {
        var integrationTestsContext = _scenarioContext.Get<IntegrationTestsContext>(ScenarioContextKeys.IntegrationTestsContext);
        var category = await integrationTestsContext.DbContext.Categories.SingleOrDefaultAsync(x => x.Name == categoryName);
        category.Should().NotBeNull();
        var categoryId = category.Id;
        _scenarioContext.Set(categoryId, ScenarioContextKeys.CategoryId);
    }
    
    [When(@"the user sends a POST request to \/api\/categories with the following data")]
    public async Task GivenTheUserSendsAPOSTRequestToApiCategoriesWithTheFollowingData(Table table)
    {
        var command = table.CreateInstance<AddCategoryCommand>();
        var integrationTestsContext = _scenarioContext.Get<IntegrationTestsContext>(ScenarioContextKeys.IntegrationTestsContext);
        
        var response = await integrationTestsContext.ApiClient.PostAsJsonAsync("/api/categories", command);
        _scenarioContext.Set(response, ScenarioContextKeys.ApiResponse);
    }
    
    [When(@"the user sends a PUT request to \/api\/categories with the following data")]
    public async Task WhenTheUserSendsAPUTRequestToApiCategoriesWithTheFollowingData(Table table)
    {
        var id = _scenarioContext.Get<int>(ScenarioContextKeys.CategoryId);
        var command = table.CreateInstance<UpdateCategoryCommand>();
        command.Id = id;
        var integrationTestsContext = _scenarioContext.Get<IntegrationTestsContext>(ScenarioContextKeys.IntegrationTestsContext);
        
        var response = await integrationTestsContext.ApiClient.PutAsJsonAsync($"/api/categories", command);
        _scenarioContext.Set(response, ScenarioContextKeys.ApiResponse);
    }
    
    [Then("the category is created in database")]
    public async Task ThenTheCategoryIsCreatedInDatabase()
    {
        var response = _scenarioContext.Get<HttpResponseMessage>(ScenarioContextKeys.ApiResponse);
        response.EnsureSuccessStatusCode();
        var categoryResponse = await response.Content.ReadFromJsonAsync<CategoryResponse>();
        
        var integrationTestsContext = _scenarioContext.Get<IntegrationTestsContext>(ScenarioContextKeys.IntegrationTestsContext);
        var category = integrationTestsContext.DbContext.Categories.SingleOrDefault(x => x.Id == categoryResponse.Id);

        category.Should().NotBeNull();
    }
    
    [Then("name of the category is updated in database")]
    public async Task ThenNameOfTheCategoryIsUpdatedInDatabase()
    {
        var response = _scenarioContext.Get<HttpResponseMessage>(ScenarioContextKeys.ApiResponse);
        response.EnsureSuccessStatusCode();
        var categoryResponse = await response.Content.ReadFromJsonAsync<CategoryResponse>();
        categoryResponse.Should().NotBeNull();
        
        var integrationTestsContext = _scenarioContext.Get<IntegrationTestsContext>(ScenarioContextKeys.IntegrationTestsContext);
        integrationTestsContext.DbContext.ChangeTracker.Clear();
        var category = integrationTestsContext.DbContext.Categories.SingleOrDefault(x => x.Id == categoryResponse.Id);
        category.Should().NotBeNull();

        category.Name.Should().Be(categoryResponse.Name);
    }
}