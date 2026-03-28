using MVVM_Example.ViewModels;
using MVVM_Example.Views;

namespace MVVM_Example;

/// <summary>
/// Программа демонстрирует паттерн MVVM на примере To-Do List приложения
/// 
/// Архитектура MVVM:
/// - Model: Task, TaskRepository (данные и логика хранения)
/// - ViewModel: TaskViewModel (бизнес-логика, команды, Observable)
/// - View: ConsoleView (пользовательский интерфейс)
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        Console.WriteLine("Запуск MVVM To-Do List приложения...");
        Console.WriteLine();

        // Создание ViewModel
        var viewModel = new TaskViewModel();

        // Создание View и связывание с ViewModel
        var view = new ConsoleView(viewModel);

        // Запуск приложения
        view.Run();
    }
}
