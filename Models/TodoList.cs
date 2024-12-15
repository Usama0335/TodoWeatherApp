using System.Collections.Generic;

namespace MauiApp1.Models
{
    public class TodoList
    {
        public int Id { get; set; } // Primary key for the list

        public string Name { get; set; } = string.Empty; // Name of the to-do list (e.g., "Work", "Home")

        // Navigation property to associate multiple TodoItems with this TodoList
        public ICollection<TodoItem> Items { get; set; } = new List<TodoItem>();
    }
}
