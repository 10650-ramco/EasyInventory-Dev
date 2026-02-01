using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.WPF.Services;
using Presentation.WPF.ViewModels;
using Presentation.WPF.Views;
using System.Windows;

namespace Presentation.WPF
{
    public partial class App : System.Windows.Application
    {
        private IServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            var configuration = BuildConfiguration();

            ConfigureServices(services, configuration);

            _serviceProvider = services.BuildServiceProvider();

            ShowLoginWindow();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }

            base.OnExit(e);
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private static void ConfigureServices(
            IServiceCollection services,
            IConfiguration configuration)
        {
            var provider = configuration["Database:Provider"];

            services.AddDbContext<AppDbContext>(options =>
            {
                switch (provider)
                {
                    case "SqlServer":
                        var sqlServerConn = configuration["Database:ConnectionStrings:SqlServer"];
                        options.UseSqlServer(sqlServerConn, sql => sql.EnableRetryOnFailure());
                        break;

                    case "MSSQL":
                        var mssqlConn = configuration["Database:ConnectionStrings:MSSQL"];
                        options.UseSqlServer(mssqlConn, sql => sql.EnableRetryOnFailure());
                        break;

                    case "MySql":
                        var mySqlConn = configuration["Database:ConnectionStrings:MySql"];
                        options.UseMySql(mySqlConn, ServerVersion.AutoDetect(mySqlConn));
                        break;

                    default:
                        throw new InvalidOperationException(
                            $"Unsupported database provider: {provider}");
                }
            });

            // Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services 
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserService, UserService>();

            // Window / Navigation
            services.AddSingleton<IWindowService, WindowService>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<EmployeeViewModel>();
            services.AddTransient<DashboardView>();
            services.AddTransient<InventoryView>();
            services.AddTransient<SalesView>();
            services.AddTransient<PurchaseView>();
            services.AddTransient<ReportsView>();

            // View
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<MainWindow>();
            //services.AddTransient<EmployeeView>();
        }

        private void ShowLoginWindow()
        {
            var loginWindow = _serviceProvider!.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
    }
}
