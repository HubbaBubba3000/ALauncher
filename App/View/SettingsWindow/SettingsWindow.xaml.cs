using System.Windows;
using System.Windows.Input;
using System;

namespace ALauncher.View;

public partial class SettingsWindow : Window, IDisposable
{
    public SettingsWindow()
    {
        InitializeComponent();
    }
    public void Close(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    public void Dispose() { }

    public void MoveWindow(object sender, MouseEventArgs e)
    {
        if (Mouse.LeftButton == MouseButtonState.Pressed)
            Window.GetWindow(this).DragMove();
    }
}