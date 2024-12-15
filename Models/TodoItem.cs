using System.ComponentModel;

namespace MauiApp1.Models
{
    public class TodoItem : INotifyPropertyChanged
    {
        public int Id { get; set; } // Add this line to define the primary key

        private bool _isCompleted;
        private string _title = string.Empty;

        // Title of the task
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        // Task completion status
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        // Foreign key for the associated TodoList
        public int TodoListId { get; set; }

        // Navigation property for EF Core to understand the relationship
        public TodoList TodoList { get; set; }

        // Event to notify the UI of property changes
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
