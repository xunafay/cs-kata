using System.Text.Json.Serialization;

namespace BeeSpeed.Console.Objects;

public class Setup
{
    [JsonPropertyName("day")]
    public Day Day { get; set; }
    [JsonPropertyName("tasks")]
    public IEnumerable<BarryTask> Tasks { get; set; }
    [JsonPropertyName("resources")]
    public IEnumerable<Resource> Resources { get; set; }
}