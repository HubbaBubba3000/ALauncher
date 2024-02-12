using System.Windows.Controls;
using System.Windows;
using ALauncher.ViewModel;
using System.Windows.Input;
using System;
using System.ComponentModel;

namespace ALauncher.View;

public partial class SettingsWindow : Window, IDisposable {
    public SettingsWindow(SettingsVM settingsVM) {
        this.DataContext = settingsVM;
        InitializeComponent();
    }
    public void Close(object sender, RoutedEventArgs e) {
        this.Close();
    }

    public void Dispose() {}

    public void MoveWindow(object sender, MouseEventArgs e) {
        if (Mouse.LeftButton == MouseButtonState.Pressed)
            Window.GetWindow(this).DragMove();
    }
}