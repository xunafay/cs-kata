// See https://aka.ms/new-console-template for more information

using BeeSpeed.Console;
using BeeSpeed.Console.Objects;

Console.WriteLine("Hello, World!");

var hi = new JSONReader().GetSetup();
Console.WriteLine("hi: " + hi);


IEnumerable<Result> result=[
    new Result()
    {
        ResourceId = "res",
        TaskId = "task",
        Time = new TimeOnly(8, 30)
    }
];
new JSONReader().SerializeResult(result);