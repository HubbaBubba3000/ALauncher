using System;
using System.Windows.Controls.Primitives;
using System.Windows;
using ALauncher.ViewModel;
using ALauncher.View;
using System.ComponentModel;
using ALauncher.Core;
using ALauncher.Data;
using System.Windows.Documents;

namespace ALauncher
{
    public partial class MainWindow : Window, IDisposable
    {
        private SettingsManager Settings;
        private SettingsConfig config
        {
            get => (SettingsConfig)Settings.GetConfig;
        }
        public MainWindow(MainVM m, SettingsManager sm)
        {
            DataContext = m;
            Settings = sm;
            Closing += OnWindowClosing;
            Settings.SettingsChanged += ChangeSettings;
            InitializeComponent();
            Loaded += OnLoaded;
        }
        private Thumb LeftPanelThumb = new Thumb()
        {
            Style = Application.Current.FindResource("LeftPanelThumbStyle") as Style
        };
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(dockpanel).Add(new ResizeAdorner(leftpanel, LeftPanelThumb));
        }
        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Loaded -= OnLoaded;
            Settings.SettingsChanged -= ChangeSettings;
            Closing -= OnWindowClosing;
            Dispose();
        }

        private void ChangeSettings()
        {
            config.WindowWidth = (int)Width;
            config.WindowHeight = (int)Height;
            config.ControlPanelWidth = (int)leftpanel.Width;
        }

        public void Dispose()
        {
            Application.Current.MainWindow = null;
            LeftPanelThumb = null;
            GC.Collect();
        }
    }
}
