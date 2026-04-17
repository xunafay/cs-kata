// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using BeeSpeed.Console;
using BeeSpeed.Console.Engine;
using BeeSpeed.Console.Objects;

var setup = new JSONReader().GetSetup();
var engine = new Engine(setup.Tasks, setup.Resources, setup.Day.StartTime, setup.Day.EndTime);
var results = engine.Run();
Console.WriteLine(engine.Results.Count());

var json = new JSONReader().SerializeResult(new ResultList()
{
    Results = results
});
var f = File.Open("./result.json", FileMode.Create);
f.Write(Encoding.UTF8.GetBytes(json));
f.Flush();

var f2 = File.Open("./result-debug.json", FileMode.Create);
f2.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(engine.Results, new JsonSerializerOptions()
{
    WriteIndented = true,
})));
f2.Flush();
