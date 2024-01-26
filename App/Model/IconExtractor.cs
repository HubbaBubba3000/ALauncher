
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ALauncher.Model;
    /// <summary>
    ///  class for extract icons from exe/dll and open icon selector 
    /// </summary>
public class IconExtractor {
    public static ImageSource GetIcon(string path) {
        Icon? icon = Icon.ExtractAssociatedIcon(path);
        return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle, 
                new Int32Rect(0,0,icon.Width,icon.Height),
                BitmapSizeOptions.FromEmptyOptions());
    }
}