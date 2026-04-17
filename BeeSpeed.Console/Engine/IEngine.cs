using BeeSpeed.Console.Objects;

namespace BeeSpeed.Console.Engine;

public interface IEngine
{
    IEnumerable<Result> Run();
}
