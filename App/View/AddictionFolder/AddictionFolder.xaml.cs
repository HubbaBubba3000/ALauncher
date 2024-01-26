using ALauncher.Data;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
namespace ALauncher.View;

public partial class AddictionFolder : Window, IDisposable {
    public bool IsAdd = false;
    public Folder GetFolder;
    public AddictionFolder() {
        InitializeComponent();
    }
    public void AddFolder(object sender, RoutedEventArgs e) {
        IsAdd = true;
        GetFolder = new Folder() { Name = NameBox.Text, Items = new(1) };
        this.Close();
    }
     public void Close(object sender, RoutedEventArgs e) {
        GetFolder = null;
        this.Close();
    }

    public void Dispose()
    {
        //
    }

    public void MoveWindow(object sender, MouseEventArgs e) {
        if (Mouse.LeftButton == MouseButtonState.Pressed)
            Window.GetWindow(this).DragMove();
    }

}