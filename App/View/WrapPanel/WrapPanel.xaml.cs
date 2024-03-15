using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ALauncher.View;

public partial class WrapPanel : UserControl
{
    public WrapPanel()
    {
        InitializeComponent();
    }
    public void ButtonExit(object? sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();

        Application.Current.Shutdown();
    }
    public void MoveWindow(object sender, MouseEventArgs e)
    {
        if (Mouse.LeftButton == MouseButtonState.Pressed)
            Window.GetWindow(this).DragMove();
    }

}