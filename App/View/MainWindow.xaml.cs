using System;
using System.Windows.Controls.Primitives;
using System.Windows;
using ALauncher.ViewModel;
using System.Windows.Documents;
using ALauncher.View;
using System.ComponentModel;

namespace ALauncher
{
    public partial class MainWindow : Window, IDisposable
    {
        public MainWindow(MainVM m)
        {
            DataContext = m;
            Closing += OnWindowClosing;
            InitializeComponent();
            Loaded += OnLoaded;
        }
        private Thumb LeftPanelThumb = new Thumb() {
           Style = Application.Current.FindResource("LeftPanelThumbStyle") as Style
        };
        private void OnLoaded(object sender, RoutedEventArgs e) {
            ((MainVM)DataContext).InitSettings(this);
            leftpanel.Width = ((MainVM)DataContext).ControlPanelWidth;
            AdornerLayer.GetAdornerLayer(dockpanel).Add(new ResizeAdorner(leftpanel, LeftPanelThumb, 
            (obj, e) => {
                ((MainVM)DataContext).ControlPanelWidth = (int)leftpanel.Width;
            }));
        }
        private void OnWindowClosing(object sender, CancelEventArgs e) {
            ((MainVM)DataContext).OnClosing();
            Dispose();
        }

        public void Dispose()
        {
            Application.Current.MainWindow = null;
            LeftPanelThumb = null;
            GC.Collect();
        }
    }
}
