using Microsoft.EntityFrameworkCore;
using MauiApp1.Models;

namespace MauiApp1.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoList>()
                .HasMany(t => t.Items)
                .WithOne(i => i.TodoList)
                .HasForeignKey(i => i.TodoListId);

            base.OnModelCreating(modelBuilder);
        }
    }

}
