using System.Collections.ObjectModel;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1
{
    public partial class TodoPage : ContentPage
    {
        private readonly TodoRepository _repository;
        public ObservableCollection<TodoItem> Todos { get; set; }

        public TodoPage(TodoRepository repository)
        {
            InitializeComponent();
            _repository = repository;
            Todos = new ObservableCollection<TodoItem>();
            TodoCollectionView.ItemsSource = Todos;

            // Load saved tasks from the database
            LoadTodosFromDatabase();
        }

        private async void LoadTodosFromDatabase()
        {
            try
            {
                var items = await _repository.GetTodoItemsAsync();
                foreach (var item in items)
                {
                    Todos.Add(item);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load tasks: {ex.Message}", "OK");
            }
        }

        private async void OnAddTodoClicked(object sender, EventArgs e)
        {
            string title = NewTodoEntry.Text?.Trim();
            if (!string.IsNullOrEmpty(title))
            {
                // Create new TodoItem
                var newTodo = new TodoItem { Title = title, IsCompleted = false };

                try
                {
                    // Save to database
                    await _repository.AddTodoItemAsync(newTodo);

                    // Add to the observable collection to reflect in the UI
                    Todos.Add(newTodo);
                    NewTodoEntry.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to add task: {ex.Message}", "OK");
                }
            }
        }

        private async void OnCompleteTodoClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var todoItem = (TodoItem)button.BindingContext;

            if (todoItem != null)
            {
                try
                {
                    // Toggle IsCompleted status
                    todoItem.IsCompleted = !todoItem.IsCompleted;

                    // Update in the database
                    await _repository.UpdateTodoItemAsync(todoItem);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to update task: {ex.Message}", "OK");
                }
            }
        }

        private async void OnDeleteTodoClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var todoItem = (TodoItem)button.BindingContext;

            if (todoItem != null && Todos.Contains(todoItem))
            {
                try
                {
                    // Remove from the database
                    await _repository.DeleteTodoItemAsync(todoItem);

                    // Remove from the observable collection
                    Todos.Remove(todoItem);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to delete task: {ex.Message}", "OK");
                }
            }
        }

        private async void OnNavigateToWeatherPage(object sender, EventArgs e)
        {
            // Navigate back to the Weather Page
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
