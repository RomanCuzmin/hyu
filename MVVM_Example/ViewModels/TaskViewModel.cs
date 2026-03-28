using System.Collections.ObjectModel;
using System.Windows.Input;
using MVVM_Example.Core;
using MVVM_Example.Models;

namespace MVVM_Example.ViewModels
{
    public class TaskViewModel : ObservableObject
    {
        private readonly TaskRepository _repository;
        private string _newTaskTitle = string.Empty;
        private string _newTaskDescription = string.Empty;
        private TaskItem? _selectedTask;

        public TaskViewModel()
        {
            _repository = new TaskRepository();
            
            AddTaskCommand = new RelayCommand(AddTask, _ => !string.IsNullOrWhiteSpace(NewTaskTitle));
            ToggleTaskCommand = new RelayCommand(ToggleTask);
            RemoveTaskCommand = new RelayCommand(RemoveTask);
            ClearCompletedCommand = new RelayCommand(_ => _repository.ClearCompleted());
            DeleteSelectedCommand = new RelayCommand(_ => SelectedTask = null, _ => SelectedTask != null);
        }

        public ObservableCollection<TaskItem> Tasks => _repository.Tasks;

        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set => SetProperty(ref _newTaskTitle, value);
        }

        public string NewTaskDescription
        {
            get => _newTaskDescription;
            set => SetProperty(ref _newTaskDescription, value);
        }

        public TaskItem? SelectedTask
        {
            get => _selectedTask;
            set => SetProperty(ref _selectedTask, value);
        }

        public ICommand AddTaskCommand { get; }
        public ICommand ToggleTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }
        public ICommand ClearCompletedCommand { get; }
        public ICommand DeleteSelectedCommand { get; }

        private void AddTask(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(NewTaskTitle))
                return;

            _repository.AddTask(NewTaskTitle, NewTaskDescription);
            NewTaskTitle = string.Empty;
            NewTaskDescription = string.Empty;
        }

        private void ToggleTask(object? parameter)
        {
            if (parameter is TaskItem task)
            {
                _repository.ToggleTask(task.Id);
            }
        }

        private void RemoveTask(object? parameter)
        {
            if (parameter is TaskItem task)
            {
                _repository.RemoveTask(task.Id);
            }
        }
    }
}
