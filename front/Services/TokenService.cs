// Token serivice to manage authentication by generating and validating tokens.
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;

public class TokenService
{
    private readonly HttpClient _httpClient;

    public TokenService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> GetTokenAsync()
    {
        string serializedString = JsonSerializer.Serialize(new { email = "eve.holt@reqres.in", password = "password" });
        var requestContent = new StringContent(serializedString, Encoding.UTF8, "application/json");
        requestContent.Headers.Add("x-api-key", "reqres-free-v1");

        var response = await _httpClient.PostAsync("https://reqres.in/api/login", requestContent);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        return result.GetProperty("token").GetString();
    }
}