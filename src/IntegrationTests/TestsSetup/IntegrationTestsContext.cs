  using Infrastructure;

  namespace IntegrationTests.TestsSetup;

public class IntegrationTestsContext
{
    public HttpClient ApiClient { get; }
    public PengaDbContext DbContext { get; }
    public IntegrationTestsContext()
    {
        var factory = new CustomWebApplicationFactory();
        ApiClient = factory.CreateClient();
        DbContext = factory.DbContext;
    }
    
}