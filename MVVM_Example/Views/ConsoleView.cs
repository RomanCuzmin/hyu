using MVVM_Example.ViewModels;

namespace MVVM_Example.Views;

/// <summary>
/// View - консольное представление для демонстрации MVVM
/// </summary>
public class ConsoleView
{
    private readonly TaskViewModel _viewModel;

    public ConsoleView(TaskViewModel viewModel)
    {
        _viewModel = viewModel;
        
        // Подписка на изменения ViewModel (Data Binding в консольном приложении)
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        _viewModel.Tasks.CollectionChanged += (s, e) => RenderTasks();
    }

    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TaskViewModel.StatusMessage))
        {
            RenderStatus();
        }
    }

    public void Run()
    {
        Console.Clear();
        RenderHeader();
        RenderTasks();
        RenderStatus();
        RenderMenu();

        while (true)
        {
            Console.Write("\nВведите команду (h - помощь, q - выход): ");
            var input = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(input))
                continue;

            switch (input[0])
            {
                case 'q':
                    Console.WriteLine("Выход из приложения...");
                    return;

                case 'h':
                    RenderMenu();
                    break;

                case '1':
                    AddTask();
                    break;

                case '2':
                    ToggleTask();
                    break;

                case '3':
                    RemoveTask();
                    break;

                case '4':
                    ClearCompleted();
                    break;

                case '5':
                    ShowTasks();
                    break;

                default:
                    Console.WriteLine("Неизвестная команда. Нажмите 'h' для помощи.");
                    break;
            }
        }
    }

    private void AddTask()
    {
        Console.Write("Введите название задачи: ");
        var title = Console.ReadLine();
        
        if (!string.IsNullOrWhiteSpace(title))
        {
            _viewModel.NewTaskTitle = title;
            if (_viewModel.AddTaskCommand.CanExecute(null))
            {
                _viewModel.AddTaskCommand.Execute(null);
            }
        }
        else
        {
            Console.WriteLine("Название задачи не может быть пустым!");
        }
    }

    private void ToggleTask()
    {
        Console.Write("Введите ID задачи для переключения статуса: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = _viewModel.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _viewModel.ToggleTaskCommand.Execute(task);
            }
            else
            {
                Console.WriteLine($"Задача с ID {id} не найдена!");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ID!");
        }
    }

    private void RemoveTask()
    {
        Console.Write("Введите ID задачи для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = _viewModel.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _viewModel.RemoveTaskCommand.Execute(task);
            }
            else
            {
                Console.WriteLine($"Задача с ID {id} не найдена!");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ID!");
        }
    }

    private void ClearCompleted()
    {
        _viewModel.ClearCompletedCommand.Execute(null);
    }

    private void ShowTasks()
    {
        RenderTasks();
    }

    private void RenderHeader()
    {
        Console.WriteLine(new string('=', 50));
        Console.WriteLine("       TO-DO LIST приложение (MVVM Pattern)");
        Console.WriteLine(new string('=', 50));
    }

    private void RenderTasks()
    {
        Console.WriteLine("\n--- ЗАДАЧИ ---");
        
        if (!_viewModel.Tasks.Any())
        {
            Console.WriteLine("Нет задач. Добавьте новую задачу!");
        }
        else
        {
            foreach (var task in _viewModel.Tasks)
            {
                Console.WriteLine($"  {task.Id}. {task}");
            }
        }
    }

    private void RenderStatus()
    {
        Console.WriteLine($"\n[{_viewModel.StatusMessage}]");
    }

    private void RenderMenu()
    {
        Console.WriteLine(@"
--- МЕНЮ ---
1. Добавить задачу
2. Переключить статус задачи
3. Удалить задачу
4. Очистить выполненные
5. Показать все задачи
h. Помощь
q. Выход");
    }
}
