using System.Net;
using IntegrationTests.Seeds;
using FluentAssertions;
using IntegrationTests.TestsSetup;
using Reqnroll;

namespace IntegrationTests.Steps;

[Binding]
public class ApiStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;

    public ApiStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }
    
    [Given(@"the API is running")]
    public void GivenTheApiIsRunning()
    {
        var integrationTestsContext = _scenarioContext.Get<IntegrationTestsContext>(ScenarioContextKeys.IntegrationTestsContext);
        integrationTestsContext.ApiClient.Should().NotBeNull();
    }

    [Then(@"the response status code should be (.*)")]
    public void ThenTheResponseStatusCodeShouldBe(HttpStatusCode expectedStatusCode)
    {
        var response = _scenarioContext.Get<HttpResponseMessage>(ScenarioContextKeys.ApiResponse);
        response.StatusCode.Should().Be(expectedStatusCode);
    }
}