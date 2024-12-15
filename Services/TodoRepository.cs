using Microsoft.EntityFrameworkCore;
using MauiApp1.Models;
using MauiApp1.Persistence;

namespace MauiApp1.Services
{
    public class TodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all TodoItems
        public async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // Add a new TodoItem
        public async Task AddTodoItemAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
        }

        // Update an existing TodoItem
        public async Task UpdateTodoItemAsync(TodoItem todoItem)
        {
            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();
        }

        // Delete a TodoItem
        public async Task DeleteTodoItemAsync(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }
    }
}
