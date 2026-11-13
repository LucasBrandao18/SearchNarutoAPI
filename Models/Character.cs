using System.Text.Json;

namespace SearchNarutoAPI.Models;

public sealed class CharacterPage
{
    public List<Character> Characters { get; set; } = [];
    public int Total { get; set; }
}

public sealed class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<string> Images { get; set; } = [];
    public List<string> Jutsu { get; set; } = [];
    public PersonalInfo? Personal { get; set; }
    public RankInfo? Rank { get; set; }
}

public sealed class PersonalInfo
{
    public JsonElement Clan { get; set; }
    public JsonElement Affiliation { get; set; }

    public static string FormatNames(JsonElement field) => field.ValueKind switch
    {
        JsonValueKind.String => field.GetString() ?? "",
        JsonValueKind.Array => string.Join(", ", field.EnumerateArray()
            .Where(item => item.ValueKind == JsonValueKind.String)
            .Select(item => item.GetString())),
        _ => ""
    };
}

public sealed class RankInfo
{
    public Dictionary<string, string>? NinjaRank { get; set; }
}
