namespace BeeSpeed.Console.Engine;

public enum Priority
{
    Low = 0,
    Normal = 1,
    High = 2
}

public interface ITask
{
    public Priority Priority { get; }
    public string RequiredSkill { get; }
    public TimeOnly Deadline { get; }
    public int DurationMinutes { get; }
}

public interface IResource
{
    public IEnumerable<string> Skills { get; }
    public TimeOnly AvailableFrom { get; }
    public TimeOnly AvailableUntil { get; }
    public int MaxWorkMinutes { get; set; }
    public string Id { get; }
}

public interface IResult
{
    public ITask Task { get; }
    public IResource Resource { get; }
    public TimeOnly Start { get; }
}

public class Result : IResult
{
    public ITask Task { get; }
    public IResource Resource { get; }
    public TimeOnly Start { get; }

    public Result(ITask task, IResource resource, TimeOnly start)
    {
        Task = task;
        Resource = resource;
        Start = start;
    }
}

public class Engine : IEngine
{
    public IEnumerable<ITask> Tasks { get; set; }
    public IEnumerable<IResource> Resources { get; set; }

    public DateTime Start { get; }
    public DateTime End { get; }

    public IEnumerable<IResult> Results { get; set; } = [];


    public Engine(IEnumerable<ITask> tasks, IEnumerable<IResource> resources, DateTime start, DateTime end)
    {
        Tasks = tasks;
        Resources = resources;
        Start = start;
        End = end;
    }

    public void Run()
    {
        Tasks = Tasks.OrderByDescending(task => task.Priority).ThenBy(task => task.Deadline);
        foreach (var task in Tasks)
        {
            var resources = Resources
                .Where(r => r.Skills.Contains(task.RequiredSkill))
                .Where(r => r.AvailableFrom <= task.Deadline.AddMinutes(-task.DurationMinutes) && r.AvailableUntil >= task.Deadline)
                .Where(r => r.MaxWorkMinutes >= task.DurationMinutes) // TODO: modify MaxWorkMinutes
                .OrderByDescending(r => r.MaxWorkMinutes).ThenBy(r => r.Skills.Count());

            foreach (var resource in resources)
            {
                var slot = FindFirstAvailableSlot(resource, task.DurationMinutes);
                if (slot is not null)
                {
                    Results = Results.Append(new Result(task, resource, slot.Value));
                    resource.MaxWorkMinutes -= task.DurationMinutes;

                    break;
                }
            }
        }
    }

    private TimeOnly? FindFirstAvailableSlot(IResource resource, int duration)
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
