using System.Windows.Controls;
using ALauncher.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace ALauncher
{
     public partial class App : Application
    {
        IHost host;
        public App() {
            host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureLogging (logging => {
                    logging.AddEventLog();
                })
                .ConfigureServices( service => {
                    service.AddSingleton<ControlPanelVM>();
                    service.AddSingleton<WrapPanelVM>();
                    service.AddSingleton<MainVM>();
                    service.AddSingleton<MainWindow>();
                }) ;
        }  
        private void OnStartup(object? sender, StartupEventArgs e) {
            
            host.Start();
            Window? window = host.Services.GetService<MainWindow>();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            host.StopAsync();
            host.Dispose();

            base.OnExit(e);
        }

    }
}
