using System.Text.Json.Serialization;

namespace BeeSpeed.Console.Objects;

public class ResultList
{
    [JsonPropertyName("results")]
    public IEnumerable<Result> Results { get; set; }
}