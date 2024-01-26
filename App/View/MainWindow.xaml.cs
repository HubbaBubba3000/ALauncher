using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ALauncher.ViewModel;
using System.Windows.Documents;
using ALauncher.View;

namespace ALauncher
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainVM m)
        {
            this.DataContext = m;
            InitializeComponent();
            Loaded += OnLoaded;
        }
        private void OnLoaded(object sender, RoutedEventArgs e) {
            AdornerLayer.GetAdornerLayer(dockpanel).Add(new ResizeAdorner(leftpanel));
        }

    }
}
