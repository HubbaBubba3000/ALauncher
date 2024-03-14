using ALauncher.ViewModel;
using ALauncher.Model;
using ALauncher.Data;
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
        public App() {
            ShutdownMode=ShutdownMode.OnExplicitShutdown;
            container = new(rules => rules.WithoutThrowIfDependencyHasShorterReuseLifespan()
                                            .WithoutFuncAndLazyWithoutRegistration()
                            ,scopeContext: new AsyncExecutionFlowScopeContext());
        }
        public void RegisterContainer() {
            container.Register<CommandWrapper>(Reuse.Singleton);
            container.Register<IconPackManager>(Reuse.Singleton);
            container.Register<FolderManager>(Reuse.Singleton);
            container.Register<SettingsManager>(Reuse.Singleton);
            container.Register<MainWindow>(Reuse.Transient,setup: Setup.With(allowDisposableTransient: true));
            container.Register<SettingsVM>(Reuse.Singleton);
            container.Register<SettingsService>(Reuse.Singleton);
            container.Register<AddItemService>(Reuse.Singleton);
            container.Register<AddFolderService>(Reuse.Singleton);
            container.Register<BottomPanelVM>(Reuse.Singleton);
            container.Register<ControlPanelVM>(Reuse.Singleton);
            container.Register<WrapPanelVM>(Reuse.Singleton);
            container.Register<MainVM>(Reuse.Singleton);
            container.Register<Logger>(Reuse.Singleton);
            container.Register<TrayVM>(Reuse.Singleton);
        }

        public static event LanguageHandler LanguageChanged;
        public delegate void LanguageHandler();
        
        public void SetLanguage(Localisation local = Localisation.EN) {
            if (CurrentLocal == local) return;
            ResourceDictionary dict = new();
            switch (local) {
                case Localisation.RU : 
                    if (CurrentLocalUri != null) {
                        dict.Source = CurrentLocalUri;
                        Resources.MergedDictionaries.Remove(dict);
                    }
                    CurrentLocalUri = new Uri ("Styles/RuLocal.xaml", UriKind.Relative);
                    dict.Source = CurrentLocalUri;
                    Resources.MergedDictionaries.Add(dict);
                    break;
                default : 
                    if (CurrentLocalUri != null) {
                        dict.Source = CurrentLocalUri;
                        Resources.MergedDictionaries.Remove(dict);
                    }
                    CurrentLocalUri = new Uri ("Styles/EnLocal.xaml", UriKind.Relative);
                    dict.Source = CurrentLocalUri;
                    Resources.MergedDictionaries.Add(dict);
                    break;
            }
            CurrentLocal = local;
            LanguageChanged?.Invoke();
        }

        private void OnStartup(object? sender, StartupEventArgs e) {
            RegisterContainer();
            Tray = (TaskbarIcon) FindResource("NotifyIcon");
            Tray.DataContext = container.Resolve<TrayVM>();
            MainWindow = container.Resolve<MainWindow>();
            MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Tray.Dispose();
            container.Dispose();
            base.OnExit(e);
        }

    }
}
