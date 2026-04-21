using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportEventApp.Data;
using SportEventApp.Forms;

namespace SportEventApp;

internal static class Program
{
    public static IConfiguration Configuration { get; private set; } = null!;
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    [STAThread]
    static void Main()
    {
        try
        {
            MessageBox.Show("1. Программа запущена", "Отладка");
            
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = configBuilder.Build();
            MessageBox.Show("2. Конфигурация загружена", "Отладка");
            
            var services = new ServiceCollection();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = "Host=localhost;Port=5432;Database=SportEventDB;Username=postgres;Password=1234";
            }
            MessageBox.Show("3. Строка подключения: " + connectionString, "Отладка");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddSingleton<MainForm>();
            services.AddTransient<FootballForm>();
            services.AddTransient<TennisForm>();
            
            ServiceProvider = services.BuildServiceProvider();
            MessageBox.Show("4. ServiceProvider создан", "Отладка");

            ApplicationConfiguration.Initialize();
            MessageBox.Show("5. ApplicationConfiguration инициализирован", "Отладка");

            var mainForm = ServiceProvider.GetRequiredService<MainForm>();
            MessageBox.Show("6. MainForm создан", "Отладка");
            
            Application.Run(mainForm);
            MessageBox.Show("7. Приложение завершено", "Отладка");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}\n{ex.StackTrace}", "Ошибка запуска");
        }
    }
}