// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using BeeSpeed.Console;
using BeeSpeed.Console.Engine;

Console.WriteLine("Hello, World!");

var setup = new JSONReader().GetSetup();
var engine = new Engine(setup.Tasks, setup.Resources, setup.Day.StartTime, setup.Day.EndTime);
engine.Run();
Console.WriteLine(engine.Results.Count());

var f = File.Open("./result.json", FileMode.Create);
f.Write(JsonSerializer.SerializeToUtf8Bytes(engine.Results, new JsonSerializerOptions { WriteIndented = true }));
f.Flush();

