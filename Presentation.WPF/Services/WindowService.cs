using Microsoft.Extensions.DependencyInjection;
using Presentation.WPF.Views;

namespace Presentation.WPF.Services
{
    public sealed class WindowService : IWindowService
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowMainWindow()
        {
            // Resolve MainWindow via DI
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

            System.Windows.Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }

        public void CloseLoginWindow()
        {
            // Close the currently active window (LoginWindow)
            var loginWindow = System.Windows.Application.Current.Windows
                .OfType<LoginWindow>()
                .FirstOrDefault();

            loginWindow?.Close();
        }
    }
}
