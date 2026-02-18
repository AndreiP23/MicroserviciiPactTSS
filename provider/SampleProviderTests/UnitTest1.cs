using Microsoft.AspNetCore.Mvc.Testing;
using PactNet.Verifier;

namespace SampleProviderTests;

public class UserProviderTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public UserProviderTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void VerifyPactWithConsumer()
    {
        //  Definim calea catre contractul generat de Consumer
        var pactPath = Path.Combine("..", "..", "..", "..", "..", "consumer", "SampleConsumerTests", "pacts", "FrontendApp-UserAPI.json");

        var config = new PactVerifierConfig();

        //  Cream un Verifier care va citi JSON-ul si va lovi API-ul real
        var verifier = new PactVerifier("UserAPI", config);

        //  Adresa unde ruleaza API-ul
        var providerUri = new Uri("http://localhost:5223");

        verifier
            .WithHttpEndpoint(providerUri)
            .WithFileSource(new FileInfo(pactPath))
            .Verify(); 
        // Compara raspunsul API-ului cu cel din JSON
    }
}
