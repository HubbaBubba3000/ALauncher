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
            leftpanel.Width = config.ControlPanelWidth;
            ChangeSettings();
            Loaded += OnLoaded;
        }
        Thumb LeftPanelThumb;
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LeftPanelThumb = new Thumb()
            {
                Style = this.FindResource("LeftPanelThumbStyle") as Style,
            };
            AdornerLayer.GetAdornerLayer(dockpanel).Add(new ResizeAdorner(leftpanel, LeftPanelThumb));
        }
        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            SaveSettings();
            Loaded -= OnLoaded;
            Settings.SettingsChanged -= ChangeSettings;
            Closing -= OnWindowClosing;
            Dispose();
        }
 
        private void ChangeSettings()
        {
            Width = config.WindowWidth;
            Height = config.WindowHeight;
        }
        private void SaveSettings() 
        {
            config.WindowHeight = (int)Height;
            config.WindowWidth = (int)Width;
            config.ControlPanelWidth = (int)leftpanel.Width;
            Settings.Save();
        }

        public void Dispose()
        {
            Application.Current.MainWindow = null;
            LeftPanelThumb = null;
            GC.Collect();
        }
    }
}
