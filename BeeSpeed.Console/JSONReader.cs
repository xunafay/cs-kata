using System.Text.Json;
using System.Text.Json.Serialization;
using BeeSpeed.Console.Objects;

namespace BeeSpeed.Console;

public class JSONReader
{
    public Setup GetSetup()
    {
        var stream = File.OpenRead("../../../setup.json");

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var setup = JsonSerializer.Deserialize<Setup>(stream, options);
        return setup;
    }

    public string SerializeResult(ResultList results)
        => JsonSerializer.Serialize(results);
}