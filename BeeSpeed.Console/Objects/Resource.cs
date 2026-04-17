using System.Text.Json.Serialization;

namespace BeeSpeed.Console.Objects;

public class Resource
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("availableFrom")]
    public TimeOnly AvailableFrom { get; set; }
     [JsonPropertyName("availableUntil")]
    public TimeOnly AvailableUntil { get; set; }
    [JsonPropertyName("skills")]
    public IEnumerable<string> Skills { get; set; }
    [JsonPropertyName("maxWorkMinutes")]
    public int MaxWorkMinutes { get; set; }
}