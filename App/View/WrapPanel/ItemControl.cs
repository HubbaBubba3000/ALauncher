using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ALauncher.View;

public sealed class ItemControl : Button
{
    public static DependencyProperty IconProperty;
    public static DependencyProperty PathProperty;
    public static DependencyProperty AppNameProperty;
    public static DependencyProperty IsFavoriteProperty;
    static ItemControl()
    {
        IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ItemControl));
        PathProperty = DependencyProperty.Register("Path", typeof(string), typeof(ItemControl));
        AppNameProperty = DependencyProperty.Register("AppName", typeof(string), typeof(ItemControl));
        IsFavoriteProperty = DependencyProperty.Register("IsFavorite", typeof(bool), typeof(ItemControl));
    }
    public ImageSource Icon
    {
        get { return (ImageSource)base.GetValue(IconProperty); }
        set { base.SetValue(IconProperty, value); }
    }
    public string AppName
    {
        get { return (string)base.GetValue(AppNameProperty); }
        set { base.SetValue(AppNameProperty, value); }
    }
    public string Path
    {
        get { return (string)base.GetValue(PathProperty); }
        set { base.SetValue(PathProperty, value); }
    }
    public bool IsFavorite
    {
        get { return (bool)base.GetValue(IsFavoriteProperty); }
        set { base.SetValue(IsFavoriteProperty, value); }
    }
}