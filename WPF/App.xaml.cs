using System;
using DryIoc;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using ALauncher.WPF.Controls.MainWindow;
using ALauncher.WPF.Common;
using ALauncher.Domain.Agregators;
using ALauncher.Domain.Logging;

namespace ALauncher
{
    public partial class App : Application
    {
        private TaskbarIcon Tray;
        public Container container;
        private IResolverContext scope;
        private Uri CurrentLocalUri;
        private Localisation CurrentLocal;
        public App()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            container = new(rules => rules.WithoutThrowIfDependencyHasShorterReuseLifespan(),
                            scopeContext: new AsyncExecutionFlowScopeContext());
        }
        public void RegisterContainer()
        {
            container.Register<CommandWrapper>(Reuse.Singleton);
            container.Register<AgregatorFactory>(Reuse.Singleton);

            container.Register<SettingsVM>(Reuse.Singleton);

            container.Register<MainWindow>(Reuse.Scoped);
            container.Register<BottomPanelVM>(Reuse.Scoped);
            container.Register<ControlPanelVM>(Reuse.Scoped);
            container.Register<WrapPanelVM>(Reuse.Scoped);
            container.Register<MainVM>(Reuse.Scoped);

            container.Register<Logger>(Reuse.Singleton);
            container.Register<TrayVM>(Reuse.Singleton);
        }

        public void SetLanguage(Localisation local = Localisation.EN)
        {
            if (CurrentLocalUri != null && CurrentLocal == local) return;
            ResourceDictionary dict = new();
            if (CurrentLocalUri != null)
            {
                dict.Source = CurrentLocalUri;
                Resources.MergedDictionaries.Remove(dict);
            }
            CurrentLocalUri = local switch
            {
                Localisation.RU => new Uri("Styles/RuLocal.xaml", UriKind.Relative),
                _ => new Uri("Styles/EnLocal.xaml", UriKind.Relative),
            };
            dict.Source = CurrentLocalUri;
            Resources.MergedDictionaries.Add(dict);
            CurrentLocal = local;
        }
        public void InitTray()
        {
            Tray = (TaskbarIcon)FindResource("NotifyIcon");
            Tray.DataContext = container.Resolve<TrayVM>();
            Tray.TrayPopup = new PopupTray() {DataContext = Tray.DataContext};
        }
        public void InitMainWindow()
        {
            scope = container.OpenScope();
            MainWindow = scope.Resolve<MainWindow>();
            MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainWindow.Show();
        }
        public void DisposeMainWindow()
        {
            MainWindow.Close();
            scope.Dispose();
            GC.Collect();
        }
        private void OnStartup(object? sender, StartupEventArgs e)
        {
            RegisterContainer();
            var settings = container.Resolve<SettingsManager>();
            settings.SettingsChanged += () => SetLanguage(((SettingsConfig)settings.GetConfig).Lang);
            SetLanguage(((SettingsConfig)settings.GetConfig).Lang);
            InitTray();
            if (!((SettingsConfig)settings.GetConfig).StartMinimize)
                InitMainWindow();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            scope.Dispose();
            Tray.Dispose();
            container.Dispose();
            base.OnExit(e);
        }

    }
}
