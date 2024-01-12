using System.Windows;
using System.Windows.Controls;

namespace ALauncher.View;

public partial class WrapPanel : UserControl {
    public WrapPanel() {
        InitializeComponent();
    }
    public void ButtonExit(object? sender, RoutedEventArgs e) {
            Window.GetWindow(this).Close();
        }

}