"""
MVVM Pattern Example - To-Do List Application

This example demonstrates the MVVM (Model-View-ViewModel) pattern in Python.
The application is a simple To-Do list manager.
"""

from abc import ABC, abstractmethod
from typing import List, Callable, Optional
from dataclasses import dataclass, field
from datetime import datetime


# ==================== MODEL ====================
@dataclass
class Task:
    """Model representing a task."""
    id: int
    title: str
    description: str = ""
    completed: bool = False
    created_at: datetime = field(default_factory=datetime.now)

    def __str__(self):
        status = "✓" if self.completed else "○"
        return f"[{status}] {self.title}"


class TaskRepository:
    """Repository for managing tasks (data layer)."""
    
    def __init__(self):
        self._tasks: List[Task] = []
        self._next_id: int = 1
    
    def get_all(self) -> List[Task]:
        return self._tasks.copy()
    
    def add(self, task: Task) -> Task:
        task.id = self._next_id
        self._next_id += 1
        self._tasks.append(task)
        return task
    
    def update(self, task: Task) -> bool:
        for i, t in enumerate(self._tasks):
            if t.id == task.id:
                self._tasks[i] = task
                return True
        return False
    
    def delete(self, task_id: int) -> bool:
        for i, task in enumerate(self._tasks):
            if task.id == task_id:
                del self._tasks[i]
                return True
        return False
    
    def get_by_id(self, task_id: int) -> Optional[Task]:
        for task in self._tasks:
            if task.id == task_id:
                return task
        return None


# ==================== VIEWMODEL ====================
class Observable:
    """Base class for observable objects."""
    
    def __init__(self):
        self._observers: List[Callable] = []
    
    def add_observer(self, callback: Callable):
        self._observers.append(callback)
    
    def remove_observer(self, callback: Callable):
        self._observers.remove(callback)
    
    def notify(self):
        for observer in self._observers:
            observer()


class TaskViewModel(Observable):
    """ViewModel that handles business logic and data binding."""
    
    def __init__(self, repository: TaskRepository):
        super().__init__()
        self._repository = repository
        self._tasks: List[Task] = []
        self._new_task_title: str = ""
        self._new_task_description: str = ""
        self._selected_task: Optional[Task] = None
        self._filter_completed: bool = False
        
        self._load_tasks()
    
    def _load_tasks(self):
        """Load tasks from repository."""
        self._tasks = self._repository.get_all()
        self.notify()
    
    @property
    def tasks(self) -> List[Task]:
        """Get filtered list of tasks."""
        if self._filter_completed:
            return [t for t in self._tasks if t.completed]
        return self._tasks
    
    @property
    def new_task_title(self) -> str:
        return self._new_task_title
    
    @new_task_title.setter
    def new_task_title(self, value: str):
        self._new_task_title = value
        self.notify()
    
    @property
    def new_task_description(self) -> str:
        return self._new_task_description
    
    @new_task_description.setter
    def new_task_description(self, value: str):
        self._new_task_description = value
        self.notify()
    
    @property
    def selected_task(self) -> Optional[Task]:
        return self._selected_task
    
    @selected_task.setter
    def selected_task(self, task: Optional[Task]):
        self._selected_task = task
        self.notify()
    
    @property
    def filter_completed(self) -> bool:
        return self._filter_completed
    
    @filter_completed.setter
    def filter_completed(self, value: bool):
        self._filter_completed = value
        self._load_tasks()
    
    def add_task(self):
        """Add a new task."""
        if not self._new_task_title.strip():
            return
        
        task = Task(
            id=0,  # Will be set by repository
            title=self._new_task_title.strip(),
            description=self._new_task_description.strip()
        )
        
        self._repository.add(task)
        self._new_task_title = ""
        self._new_task_description = ""
        self._load_tasks()
    
    def toggle_task_completion(self, task_id: int):
        """Toggle task completion status."""
        task = self._repository.get_by_id(task_id)
        if task:
            task.completed = not task.completed
            self._repository.update(task)
            self._load_tasks()
    
    def delete_task(self, task_id: int):
        """Delete a task."""
        self._repository.delete(task_id)
        self._load_tasks()
    
    def get_task_count(self) -> int:
        return len(self._tasks)
    
    def get_completed_count(self) -> int:
        return sum(1 for t in self._tasks if t.completed)


# ==================== VIEW ====================
class View(ABC):
    """Abstract base class for views."""
    
    @abstractmethod
    def render(self):
        pass
    
    @abstractmethod
    def bind(self, viewmodel: TaskViewModel):
        pass


class ConsoleView(View):
    """Console-based view implementation."""
    
    def __init__(self):
        self._viewmodel: Optional[TaskViewModel] = None
    
    def bind(self, viewmodel: TaskViewModel):
        self._viewmodel = viewmodel
        viewmodel.add_observer(self.render)
    
    def render(self):
        """Render the UI to console."""
        if not self._viewmodel:
            return
        
        print("\n" + "=" * 50)
        print("           TO-DO LIST APPLICATION")
        print("=" * 50)
        
        # Show statistics
        total = self._viewmodel.get_task_count()
        completed = self._viewmodel.get_completed_count()
        print(f"\nTasks: {total} total, {completed} completed, {total - completed} pending")
        
        # Show filter status
        filter_status = "Completed only" if self._viewmodel.filter_completed else "All tasks"
        print(f"Filter: {filter_status}")
        
        # Show tasks
        print("\n--- Tasks ---")
        tasks = self._viewmodel.tasks
        if not tasks:
            print("No tasks found.")
        else:
            for task in tasks:
                status = "[X]" if task.completed else "[ ]"
                print(f"{task.id}. {status} {task.title}")
                if task.description:
                    print(f"   Description: {task.description}")
        
        # Show input form
        print("\n--- Add New Task ---")
        print(f"Title: {self._viewmodel.new_task_title}|")
        print(f"Description: {self._viewmodel.new_task_description}|")
        
        print("\n--- Commands ---")
        print("1. Set title")
        print("2. Set description")
        print("3. Add task")
        print("4. Toggle task completion (enter ID)")
        print("5. Delete task (enter ID)")
        print("6. Toggle filter (show completed/all)")
        print("7. Refresh")
        print("8. Exit")
        print("=" * 50)
    
    def get_input(self, prompt: str) -> str:
        return input(prompt)
    
    def run(self):
        """Main view loop."""
        if not self._viewmodel:
            raise ValueError("ViewModel not bound")
        
        while True:
            self.render()
            choice = self.get_input("\nEnter command (1-8): ").strip()
            
            if choice == "1":
                title = self.get_input("Enter task title: ")
                self._viewmodel.new_task_title = title
            
            elif choice == "2":
                desc = self.get_input("Enter task description: ")
                self._viewmodel.new_task_description = desc
            
            elif choice == "3":
                self._viewmodel.add_task()
                print("Task added!")
            
            elif choice == "4":
                try:
                    task_id = int(self.get_input("Enter task ID to toggle: "))
                    self._viewmodel.toggle_task_completion(task_id)
                    print("Task toggled!")
                except ValueError:
                    print("Invalid ID!")
            
            elif choice == "5":
                try:
                    task_id = int(self.get_input("Enter task ID to delete: "))
                    self._viewmodel.delete_task(task_id)
                    print("Task deleted!")
                except ValueError:
                    print("Invalid ID!")
            
            elif choice == "6":
                current = self._viewmodel.filter_completed
                self._viewmodel.filter_completed = not current
                print(f"Filter changed to: {'Completed only' if not current else 'All tasks'}")
            
            elif choice == "7":
                print("Refreshed!")
            
            elif choice == "8":
                print("Goodbye!")
                break
            
            else:
                print("Invalid command!")


# ==================== MAIN ====================
def main():
    """Application entry point."""
    # Create components
    repository = TaskRepository()
    viewmodel = TaskViewModel(repository)
    view = ConsoleView()
    
    # Bind view to viewmodel
    view.bind(viewmodel)
    
    # Add some sample data
    viewmodel.new_task_title = "Learn MVVM pattern"
    viewmodel.new_task_description = "Understand Model-View-ViewModel architecture"
    viewmodel.add_task()
    
    viewmodel.new_task_title = "Build a project"
    viewmodel.new_task_description = "Create something amazing"
    viewmodel.add_task()
    
    # Run the application
    view.run()


if __name__ == "__main__":
    main()
