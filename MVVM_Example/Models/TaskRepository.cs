namespace MVVM_Example.Models;

/// <summary>
/// Model - репозиторий для хранения задач (имитация базы данных)
/// </summary>
public class TaskRepository
{
    private readonly List<Task> _tasks = new();
    private int _nextId = 1;

    public IReadOnlyList<Task> GetAllTasks() => _tasks.AsReadOnly();

    public Task AddTask(string title)
    {
        var task = new Task
        {
            Id = _nextId++,
            Title = title,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };
        _tasks.Add(task);
        return task;
    }

    public void RemoveTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            _tasks.Remove(task);
        }
    }

    public Task? GetTaskById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public int GetTotalCount() => _tasks.Count;

    public int GetCompletedCount() => _tasks.Count(t => t.IsCompleted);
}
