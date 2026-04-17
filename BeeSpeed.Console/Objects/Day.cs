using System.Text.Json.Serialization;

namespace BeeSpeed.Console.Objects;

public class Day
{
    [JsonPropertyName("startTime")]
    public TimeOnly StartTime { get; set; }
    [JsonPropertyName("endTime")]
    public TimeOnly EndTime { get; set; }
}