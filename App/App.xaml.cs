using ALauncher.ViewModel;
using ALauncher.Model;
using ALauncher.Data;
using ALauncher.Core;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using DryIoc;

namespace ALauncher
{
    public partial class App : Application
    {
        private TaskbarIcon Tray;
        public Container container;
        private Uri CurrentLocalUri;
        private Localisation CurrentLocal;
        public App()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            container = new(rules => rules.WithoutThrowIfDependencyHasShorterReuseLifespan()
                            , scopeContext: new AsyncExecutionFlowScopeContext());
        }
        public void RegisterContainer()
        {
            container.Register<CommandWrapper>(Reuse.Singleton);
            container.Register<IconPackManager>(Reuse.Singleton);
            container.Register<FolderManager>(Reuse.Singleton);
            container.Register<SettingsManager>(Reuse.Singleton);
            container.Register<MainWindow>(Reuse.Transient, setup: Setup.With(allowDisposableTransient: true));
            container.Register<SettingsVM>(Reuse.Singleton);
            container.Register<SettingsFactory>(Reuse.Singleton);
            container.Register<AddictionItemFactory>(Reuse.Singleton);
            container.Register<AddictionFolderFactory>(Reuse.Singleton);
            container.Register<BottomPanelVM>(Reuse.Singleton);
            container.Register<ControlPanelVM>(Reuse.Singleton);
            container.Register<WrapPanelVM>(Reuse.Singleton);
            container.Register<MainVM>(Reuse.Singleton);
            container.Register<Logger>(Reuse.Singleton);
            container.Register<TrayVM>(Reuse.Singleton);
        }

        public void SetLanguage(Localisation local = Localisation.EN)
        {
            if (CurrentLocal == local) return;
            ResourceDictionary dict = new();
            if (CurrentLocalUri != null)
            {
                dict.Source = CurrentLocalUri;
                Resources.MergedDictionaries.Remove(dict);
            }
            switch (local)
            {
                case Localisation.RU:
                    CurrentLocalUri = new Uri("Styles/RuLocal.xaml", UriKind.Relative);
                    break;
                default:
                    CurrentLocalUri = new Uri("Styles/EnLocal.xaml", UriKind.Relative);
                    break;
            }
            dict.Source = CurrentLocalUri;
            Resources.MergedDictionaries.Add(dict);
            CurrentLocal = local;
        }

        private void OnStartup(object? sender, StartupEventArgs e)
        {
            RegisterContainer();
            Tray = (TaskbarIcon)FindResource("NotifyIcon");
            Tray.DataContext = container.Resolve<TrayVM>();
            var settings = container.Resolve<SettingsManager>();
            settings.SettingsChanged += () => SetLanguage(((SettingsConfig)settings.GetConfig).Lang);
            MainWindow = container.Resolve<MainWindow>();
            MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainWindow.Show();
            settings.FirstInit();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Tray.Dispose();
            container.Dispose();
            base.OnExit(e);
        }

    }
}
