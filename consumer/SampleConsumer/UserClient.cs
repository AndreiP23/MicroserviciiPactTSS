using SampleConsumer.Models;
using System.Net.Http.Json;

namespace SampleConsumer;

public class UserClient
{
    private readonly HttpClient _client;

    public UserClient(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://localhost:44300");
    }

    public async Task<User> GetUserById(int id)
    {
        try
        {
            var user = await _client.GetFromJsonAsync<User>($"/api/users/{id}");

            if (user == null)
            {
                throw new Exception("API-ul a răspuns, dar JSON-ul a fost null.");
            }

            return user;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Nu am putut comunica cu serverul pentru ID-ul {id}. Eroare: {ex.Message}");
        }
    }
}
