using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ALauncher.ViewModel;

namespace ALauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainVM m)
        {
            this.DataContext = m;
            InitializeComponent();
        }
    }
}
