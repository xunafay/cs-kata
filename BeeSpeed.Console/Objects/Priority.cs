using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BeeSpeed.Console.Objects;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Priority
{
    [JsonStringEnumMemberName("Hoog")]
    High = 2,
    [JsonStringEnumMemberName("Normaal")]
    Normal = 1,
    [JsonStringEnumMemberName("Laag")]
    Low = 0,
}
