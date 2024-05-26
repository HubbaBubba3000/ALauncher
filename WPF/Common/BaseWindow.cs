using System.Windows;
using System.Windows.Input;

namespace ALauncher.WPF.Common;

public abstract class BaseWindow : Window
{
    public void MoveWindow(object sender, MouseEventArgs e)
    {
        if (Mouse.LeftButton == MouseButtonState.Pressed)
            Window.GetWindow(this).DragMove();
    }
}