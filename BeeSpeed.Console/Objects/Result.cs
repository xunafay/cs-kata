using System.Text.Json.Serialization;

namespace BeeSpeed.Console.Objects;

public class Result
{
    [JsonPropertyName("task")]
    public string TaskId { get; set; }
    [JsonPropertyName("resource")]
    public string ResourceId { get; set; }
    [JsonPropertyName("time")]
    public TimeOnly Time { get; set; }
}