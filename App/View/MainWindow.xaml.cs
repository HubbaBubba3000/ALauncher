using System;
using System.Windows.Controls.Primitives;
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
            DataContext = m;
            m.LoadSettings(this);
            Closing += m.OnClosing;

            InitializeComponent();
            Loaded += OnLoaded;
        }
        private Thumb LeftPanelThumb = new Thumb() {
           Style = Application.Current.FindResource("LeftPanelThumbStyle") as Style
        };
        private void OnLoaded(object sender, RoutedEventArgs e) {
            AdornerLayer.GetAdornerLayer(dockpanel).Add(new ResizeAdorner(leftpanel, LeftPanelThumb));
        }

    }
}
