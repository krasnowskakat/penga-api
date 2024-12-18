using IntegrationTests.Seeds;
using Reqnroll;

namespace IntegrationTests.TestsSetup;

[Binding]
public class BeforeEachScenario
{
    private readonly ScenarioContext _scenarioContext;
    private IntegrationTestsContext _integrationTestsContext;
    
    public BeforeEachScenario(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _integrationTestsContext = new IntegrationTestsContext();
    }
    
    [BeforeScenario]
    public void BeforeScenario()
    {
        _scenarioContext.Set(_integrationTestsContext, ScenarioContextKeys.IntegrationTestsContext);
    }
}