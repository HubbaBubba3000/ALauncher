using System;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.ComponentModel;
using ALauncher.WPF.Common;
using ALauncher.Domain.Agregators;
using System.Windows.Documents;
using ALauncher.Domain.Agregators;
using ALauncher.Domain.ConfigRepositories;

namespace ALauncher.WPF.Controls.MainWindow;

public partial class MainWindow : Window, IDisposable
{
    private SettingsAgregator Settings;
    private ConfigSaveRepository SettingsSaver;
    public MainWindow(MainVM m, AgregatorFactory af, ConfigSaveRepository csr)
    {
        DataContext = m;
        Settings = af.GetSettings();
        SettingsSaver = csr;
        Closing += OnWindowClosing;
        InitializeComponent();
        leftpanel.Width = Settings.ControlPanelWidth;
        ChangeSettings();
        Loaded += OnLoaded;
    }
    Thumb? LeftPanelThumb;
    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        LeftPanelThumb = new Thumb()
        {
            Style = this.FindResource("LeftPanelThumbStyle") as Style,
        };
        AdornerLayer.GetAdornerLayer(dockpanel).Add(new ResizeAdorner(leftpanel, LeftPanelThumb));
    }
    private void OnWindowClosing(object? sender, CancelEventArgs e)
    {
        SaveSettings();
        Loaded -= OnLoaded;
        Closing -= OnWindowClosing;
        Dispose();
    }

    private void ChangeSettings()
    {
        Width = Settings.WindowWidth;
        Height = Settings.WindowHeight;
    }
    private void SaveSettings() 
    {
        Settings.WindowHeight = (int)Height;
        Settings.WindowWidth = (int)Width;
        Settings.ControlPanelWidth = (int)leftpanel.Width;
        
    }

    public void Dispose()
    {
        Application.Current.MainWindow = null;
        LeftPanelThumb = null;
        GC.Collect();
    }
}

