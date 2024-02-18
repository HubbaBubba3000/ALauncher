using ALauncher.Data;
using ALauncher.Model;
using System;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
namespace ALauncher.View;

public partial class AddictionItems : Window, IDisposable {
    public bool IsAdd = false;
    public Item GetItem;
    public AddictionItems() {
        InitializeComponent();
    }
    public void AddItem(object sender, RoutedEventArgs e) {
        IsAdd = true;
        if (!Path.IsPathRooted(PathBox.Text)) {
            MessageBox.Show("Path is not valid");
            return;
        }
        GetItem = new Item() { 
            AppName = NameBox.Text, 
            Path = PathBox.Text, 
            Icon = IconExtractor.GetIcon(PathBox.Text)
        };
        this.Close();
    }

    public void Browse(object sender, RoutedEventArgs e) {
        OpenFileDialog ofd = new() {
            Filter = "executable (.exe) file |*.exe",
            Multiselect = false
        };
        if (ofd.ShowDialog() == true)
            PathBox.Text = ofd.FileName;
        
    }
    public void Close(object sender, RoutedEventArgs e) {
        this.Close();
    }

    public void Dispose()
    {
        GetItem = null;
    }

    public void MoveWindow(object sender, MouseEventArgs e) {
        if (Mouse.LeftButton == MouseButtonState.Pressed)
            Window.GetWindow(this).DragMove();
    }

}