using PactNet;

namespace SampleConsumerTests;

public class UserContractTests
{
    private readonly IPactBuilderV4 _pactBuilder;

    public UserContractTests()
    {
        // 1. Definim fisierul JSON unde se va salva contractul
        var config = new PactConfig
        {
            PactDir = "../../../pacts", // Se creeaza un folder 'pacts'
            LogLevel = PactLogLevel.Debug
        };

        // 2. Initializam Pact: cine e Consumer (Frontend) si cine e Provider (UserAPI)
        var pact = Pact.V4("FrontendApp", "UserAPI", config);
        _pactBuilder = pact.WithHttpInteractions();
    }

    [Fact]
    public async Task GetUser_CreeazaContract()
    {
        // 3. Definim contractul (asteptarile)
        // ex: Daca fac GET la /api/users/666, astept 200 OK + un JSON
        _pactBuilder
            .UponReceiving("Cerere pentru detaliile userului 666")
                .WithRequest(HttpMethod.Get, "/api/users/666")
            .WillRespond()
                .WithStatus(System.Net.HttpStatusCode.OK)
                .WithHeader("Content-Type", "application/json")
                .WithJsonBody(new
                {
                    id = 666,
                    firstName = "Ravanelli",
                    lastName = "Lucifer",
                    email = "rava.lucifer@4226crangasi.com"
                });

        // 4. Ruleaza testul si genereaza JSON-ul
        await _pactBuilder.VerifyAsync(async ctx =>
        {
            var client = new HttpClient { BaseAddress = ctx.MockServerUri };
            var response = await client.GetAsync("/api/users/666");

            // Verificam daca serverul Pact a raspuns corect
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        });
    }
}