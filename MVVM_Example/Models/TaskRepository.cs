using System.Collections.ObjectModel;
using MVVM_Example.Models;

namespace MVVM_Example.Models
{
    public class TaskRepository
    {
        private readonly ObservableCollection<TaskItem> _tasks = new();
        private int _nextId = 1;

        public ObservableCollection<TaskItem> Tasks => _tasks;

        public void AddTask(string title, string description)
        {
            var task = new TaskItem
            {
                Id = _nextId++,
                Title = title,
                Description = description,
                IsCompleted = false
            };
            _tasks.Add(task);
        }

        public void ToggleTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                task.CompletedAt = task.IsCompleted ? DateTime.Now : null;
            }
        }

        public void RemoveTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }

        public void ClearCompleted()
        {
            var completedTasks = _tasks.Where(t => t.IsCompleted).ToList();
            foreach (var task in completedTasks)
            {
                _tasks.Remove(task);
            }
        }
    }
}
