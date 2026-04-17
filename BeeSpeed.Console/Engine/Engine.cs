using BeeSpeed.Console.Objects;

namespace BeeSpeed.Console.Engine;

public interface IResult
{
    BarryTask Task { get; }
    Resource Resource { get; }
    TimeOnly Start { get; }

    Result Map();
}

public class BarryResult : IResult
{
    public BarryTask Task { get; }
    public Resource Resource { get; }
    public TimeOnly Start { get; }

    public BarryResult(BarryTask task, Resource resource, TimeOnly start)
    {
        Task = task;
        Resource = resource;
        Start = start;
    }

    public Result Map()
    {
        return new Result
        {
            ResourceId = Resource.Id,
            TaskId = Task.Id,
            Time = Start
        };
    }
}

public class Engine : IEngine
{
    public IEnumerable<BarryTask> Tasks { get; set; }
    public IEnumerable<Resource> Resources { get; set; }

    public TimeOnly Start { get; }
    public TimeOnly End { get; }

    public IEnumerable<IResult> Results { get; set; } = [];


    public Engine(IEnumerable<BarryTask> tasks, IEnumerable<Resource> resources, TimeOnly start, TimeOnly end)
    {
        Tasks = tasks;
        Resources = resources;
        Start = start;
        End = end;
    }

    public IEnumerable<Result> Run()
    {
        Tasks = Tasks.OrderByDescending(task => task.Priority).ThenBy(task => task.Deadline);
        foreach (var task in Tasks)
        {
            var resources = Resources
                .Where(r => r.Skills.Contains(task.RequiredSkill))
                .Where(r => r.AvailableFrom <= task.Deadline.AddMinutes(-task.DurationMinutes))
                .Where(r => r.MaxWorkMinutes >= task.DurationMinutes) // TODO: modify MaxWorkMinutes
                .OrderByDescending(r => r.MaxWorkMinutes).ThenBy(r => r.Skills.Count());

            foreach (var resource in resources)
            {
                var slot = FindFirstAvailableSlot(resource, task.DurationMinutes);
                if (slot is not null)
                {
                    Results = Results.Append(new BarryResult(task, resource, slot.Value));
                    resource.MaxWorkMinutes -= task.DurationMinutes;

                    break;
                }
            }
        }

        return Results.Select(r => r.Map());
    }

    private TimeOnly? FindFirstAvailableSlot(Resource resource, int duration)
    {
        var occupied = Results
            .Where(result => result.Resource.Id == resource.Id)
            .OrderByDescending(result => result.Start)
            .FirstOrDefault();

        if (occupied is null)
        {
            return resource.AvailableFrom;
        }

        TimeOnly? availableSlot = null;

        var nextAvailableTime = occupied.Start.AddMinutes(occupied.Task.DurationMinutes);
        if (nextAvailableTime.AddMinutes(duration) < resource.AvailableUntil)
        {
            return nextAvailableTime;
        }

        if (availableSlot is null)
        {
            // TODO: can we nudge others?
        }

        return null;
    }
}
