namespace IntegrationTests.TestsSetup;

public class IntegrationTestsContext
{
    public HttpClient ApiClient { get; }
    
    public IntegrationTestsContext()
    {
        var factory = new CustomWebApplicationFactory();
        ApiClient = factory.CreateClient();
    }
    
}