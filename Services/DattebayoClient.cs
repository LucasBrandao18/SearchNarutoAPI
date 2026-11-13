using System.Net.Http.Json;
using SearchNarutoAPI.Models;

namespace SearchNarutoAPI.Services;

public sealed class DattebayoClient(HttpClient http)
{
    public const int PageSize = 12;

    public async Task<CharacterPage> ListAsync(string name, int page)
    {
        var path = $"characters?page={page}&limit={PageSize}";
        if (name.Length > 0)
        {
            path += $"&name={Uri.EscapeDataString(name)}";
        }

        return await http.GetFromJsonAsync<CharacterPage>(path)
            ?? throw new System.Text.Json.JsonException("Resposta vazia da API.");
    }

    public async Task<Character> FindAsync(int id) =>
        await http.GetFromJsonAsync<Character>($"characters/{id}")
            ?? throw new System.Text.Json.JsonException("Ficha vazia.");
}
