using ALauncher.Abstractions.Entities;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ALauncher.WPF.Model.Services;
/// <summary>
///  class for extract icons from exe/dll and open icon selector 
/// </summary>
public sealed class IconExtractorService
{
    public ImageSource? GetIcon(string path)
    {
        if (!Path.IsPathRooted(path)) return null;
        Icon icon = Icon.ExtractAssociatedIcon(path) ?? new(path);
        return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                new Int32Rect(0, 0, icon.Width, icon.Height),
                BitmapSizeOptions.FromEmptyOptions());
    }
}

