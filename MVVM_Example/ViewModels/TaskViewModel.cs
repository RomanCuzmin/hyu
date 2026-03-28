using System.Collections.ObjectModel;
using MVVM_Example.Core;
using MVVM_Example.Models;

namespace MVVM_Example.ViewModels;

/// <summary>
/// ViewModel - содержит бизнес-логику и данные для представления
/// </summary>
public class TaskViewModel : ObservableObject
{
    private readonly TaskRepository _repository;
    private string _newTaskTitle = string.Empty;
    private string _statusMessage = string.Empty;

    public ObservableCollection<Task> Tasks { get; }
    
    public string NewTaskTitle
    {
        get => _newTaskTitle;
        set
        {
            if (SetProperty(ref _newTaskTitle, value, nameof(NewTaskTitle)))
            {
                OnPropertyChanged(nameof(CanAddTask));
            }
        }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value, nameof(StatusMessage));
    }

    public bool CanAddTask => !string.IsNullOrWhiteSpace(NewTaskTitle);

    public int TotalCount => _repository.GetTotalCount();
    
    public int CompletedCount => _repository.GetCompletedCount();

    // Команды
    public Command AddTaskCommand { get; }
    public Command RemoveTaskCommand { get; }
    public Command ToggleTaskCommand { get; }
    public Command ClearCompletedCommand { get; }

    public TaskViewModel()
    {
        _repository = new TaskRepository();
        Tasks = new ObservableCollection<Task>();
        
        AddTaskCommand = new Command(AddTask, () => CanAddTask);
        RemoveTaskCommand = new Command(RemoveTask);
        ToggleTaskCommand = new Command(ToggleTask);
        ClearCompletedCommand = new Command(ClearCompletedTasks);

        LoadTasks();
        UpdateStatusMessage();
    }

    private void LoadTasks()
    {
        var tasks = _repository.GetAllTasks();
        foreach (var task in tasks)
        {
            Tasks.Add(task);
        }
    }

    private void AddTask()
    {
        if (string.IsNullOrWhiteSpace(NewTaskTitle))
            return;

        var task = _repository.AddTask(NewTaskTitle.Trim());
        Tasks.Add(task);
        NewTaskTitle = string.Empty;
        UpdateStatusMessage();
        
        Console.WriteLine($"✓ Задача добавлена: \"{task.Title}\"");
    }

    private void RemoveTask(object? parameter)
    {
        if (parameter is Task task)
        {
            _repository.RemoveTask(task.Id);
            Tasks.Remove(task);
            UpdateStatusMessage();
            
            Console.WriteLine($"✓ Задача удалена: \"{task.Title}\"");
        }
    }

    private void ToggleTask(object? parameter)
    {
        if (parameter is Task task)
        {
            task.IsCompleted = !task.IsCompleted;
            UpdateStatusMessage();
            
            var status = task.IsCompleted ? "выполнена" : "активна";
            Console.WriteLine($"✓ Задача \"{task.Title}\" теперь {status}");
        }
    }

    private void ClearCompletedTasks()
    {
        var completedTasks = Tasks.Where(t => t.IsCompleted).ToList();
        foreach (var task in completedTasks)
        {
            _repository.RemoveTask(task.Id);
            Tasks.Remove(task);
        }
        UpdateStatusMessage();
        
        Console.WriteLine($"✓ Удалено выполненных задач: {completedTasks.Count}");
    }

    private void UpdateStatusMessage()
    {
        var total = _repository.GetTotalCount();
        var completed = _repository.GetCompletedCount();
        var pending = total - completed;
        
        StatusMessage = $"Всего: {total} | Выполнено: {completed} | В ожидании: {pending}";
        OnPropertyChanged(nameof(TotalCount));
        OnPropertyChanged(nameof(CompletedCount));
    }
}
