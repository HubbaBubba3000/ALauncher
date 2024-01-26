using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using ALauncher.ViewModel;

namespace ALauncher.View;

public class ItemControl : Button {
    public static DependencyProperty IconProperty;
    public static DependencyProperty PathProperty;
    public static DependencyProperty AppNameProperty;
    static ItemControl() {
        IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ItemControl));
        PathProperty = DependencyProperty.Register("Path", typeof(string), typeof(ItemControl));
        AppNameProperty = DependencyProperty.Register("AppName", typeof(string), typeof(ItemControl));
        
    }
    public ImageSource Icon {
        get { return (ImageSource)base.GetValue(IconProperty); } 
        set { base.SetValue(IconProperty, value); }
    }
    public string AppName {
        get { return (string)base.GetValue(AppNameProperty); } 
        set { base.SetValue(AppNameProperty, value); }
    }
    public string Path {
        get { return (string)base.GetValue(PathProperty); } 
        set { base.SetValue(PathProperty, value); }
    }
}