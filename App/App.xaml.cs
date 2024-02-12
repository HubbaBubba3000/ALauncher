using System.Windows.Controls;
using ALauncher.ViewModel;
using ALauncher.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Windows;
using ALauncher.View;

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
                    //Models
                    service.AddSingleton<FolderManager>();
                    service.AddSingleton<SettingsManager>();
                    //ViewModels
                    service.AddSingleton<ControlPanelVM>();
                    service.AddSingleton<SettingsVM>();
                    service.AddSingleton<WrapPanelVM>();
                    service.AddSingleton<BottomPanelVM>();
                    service.AddSingleton<MainVM>();
                    //View
                    service.AddSingleton<MainWindow>();
                    service.AddScoped<SettingsWindow>();
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
