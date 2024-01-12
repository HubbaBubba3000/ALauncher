using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ALauncher.View;

public class ItemControl : Button {
    public static DependencyProperty IconProperty;
    public static DependencyProperty NameProperty;
    static ItemControl() {
        IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ItemControl));
        NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(ItemControl));
    }

    public ImageSource Icon {
        get { return (ImageSource)base.GetValue(IconProperty); } 
        set { base.SetValue(IconProperty, value); }
    }
    public string Name {
        get { return (string)base.GetValue(NameProperty); } 
        set { base.SetValue(NameProperty, value); }
    }
}