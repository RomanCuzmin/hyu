namespace MVVM_Example.Models;

/// <summary>
/// Model - представляет задачу в приложении To-Do List
/// </summary>
public class Task : ObservableObject
{
    private int _id;
    private string _title = string.Empty;
    private bool _isCompleted;
    private DateTime _createdAt;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value, nameof(Id));
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value, nameof(Title));
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => SetProperty(ref _isCompleted, value, nameof(IsCompleted));
    }

    public DateTime CreatedAt
    {
        get => _createdAt;
        set => SetProperty(ref _createdAt, value, nameof(CreatedAt));
    }

    public override string ToString()
    {
        var status = IsCompleted ? "✓" : "○";
        return $"[{status}] {Title} (создано: {CreatedAt:dd.MM.yyyy HH:mm})";
    }
}
