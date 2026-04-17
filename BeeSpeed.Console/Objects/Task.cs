using System.Text.Json.Serialization;

namespace BeeSpeed.Console.Objects;

public class Task
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("durationMinutes")]
    public int DurationMinutes { get; set; }
    [JsonPropertyName("priority")]
    public Priority Priority { get; set; }
    [JsonPropertyName("deadline")]
    public TimeOnly Deadline { get; set; }
    [JsonPropertyName("requiredSkill")]
    public string RequiredSkill { get; set; }
}