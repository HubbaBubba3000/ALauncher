
using ALauncher.ViewModel;
using ALauncher.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows.Media;

namespace ALauncher
{
    public partial class App : Application
    {
        private TaskbarIcon Tray;
        public IHost host;
        public App() {
            ShutdownMode=ShutdownMode.OnExplicitShutdown;
            host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices( service => {
                    //Models
                    service.AddSingleton<IconPackManager>();
                    service.AddSingleton<FolderManager>();
                    service.AddSingleton<SettingsManager>();
                    service.AddSingleton<Func<SettingsVM>>(provider => provider.GetService<SettingsVM>);
                    service.AddSingleton<SettingsService>();
                    service.AddSingleton<AddItemService>();
                    service.AddSingleton<AddFolderService>();
                    //ViewModels
                    service.AddSingleton<BottomPanelVM>();
                    service.AddSingleton<ControlPanelVM>();
                    service.AddSingleton<WrapPanelVM>();
                    service.AddSingleton<MainVM>();
                    service.AddSingleton<CommandWrapper>();
                    service.AddScoped<SettingsVM>();
                    service.AddSingleton<Logger>();
                    service.AddSingleton<TrayVM>();
                    //View
                    service.AddTransient<MainWindow>();
                }) ;
        }  
        private void OnStartup(object? sender, StartupEventArgs e) {
            
            host.Start();
            Tray = (TaskbarIcon) FindResource("NotifyIcon");
            Tray.DataContext = host.Services.GetService<TrayVM>();
            MainWindow = host.Services.GetService<MainWindow>();
            MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            host.StopAsync();
            host.Dispose();

            base.OnExit(e);
        }

    }
}
