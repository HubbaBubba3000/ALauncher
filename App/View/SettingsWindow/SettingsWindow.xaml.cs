using System.Windows.Controls;
using System.Windows;
using ALauncher.ViewModel;
using System.Windows.Input;

namespace ALauncher.View;

public partial class SettingsWindow : Window {
    public SettingsWindow(SettingsVM settingsVM) {
        this.DataContext = settingsVM;
        InitializeComponent();
    }

    public void MoveWindow(object sender, MouseEventArgs e) {
        if (Mouse.LeftButton == MouseButtonState.Pressed)
            Window.GetWindow(this).DragMove();
    }
}