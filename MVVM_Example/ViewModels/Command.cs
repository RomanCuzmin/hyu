using MVVM_Example.Core;

namespace MVVM_Example.ViewModels;

/// <summary>
/// Реализация команды (Command pattern) для MVVM
/// </summary>
public class Command : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<bool>? _canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public Command(Action<object?> execute, Func<bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public Command(Action execute, Func<bool>? canExecute = null)
        : this(_ => execute(), canExecute)
    {
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }

    /// <summary>
    /// Вызвать перепроверку CanExecute для всех команд
    /// </summary>
    public static void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}

/// <summary>
/// Менеджер команд для управления состоянием CanExecute
/// </summary>
public static class CommandManager
{
    public static event EventHandler? RequerySuggested;

    public static void InvalidateRequerySuggested()
    {
        RequerySuggested?.Invoke(null, EventArgs.Empty);
    }
}
