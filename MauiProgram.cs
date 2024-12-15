using Microsoft.EntityFrameworkCore;
using MauiApp1.Persistence;
using MauiApp1.Services;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register AppDbContext with dependency injection
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Filename=TodoApp.db"));

            // Register TodoRepository
            builder.Services.AddSingleton<TodoRepository>();

            // Register Pages for DI
            builder.Services.AddTransient<TodoPage>();

            return builder.Build();
        }
    }
}
