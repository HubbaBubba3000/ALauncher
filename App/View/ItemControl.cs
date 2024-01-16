using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ALauncher.View;

public class ItemControl : Button {
    public static DependencyProperty IconProperty;
    public static DependencyProperty AppNameProperty;
    static ItemControl() {
        IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ItemControl));
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
}