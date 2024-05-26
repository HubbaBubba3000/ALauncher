
using System.IO;
using System.Windows.Media.Imaging;

namespace ALauncher.WPF.Model.Converters;

public sealed class Base64ToImageConverter
{

    public static BitmapSource? Convert(string? base64)
    {
        if (string.IsNullOrEmpty(base64)) return null;
        byte[] bytes = System.Convert.FromBase64String(base64);
        using (var stream = new MemoryStream(bytes))
            return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
    }
    public static string ConvertBack(BitmapSource bitmap)
    {
        if (bitmap == null) return string.Empty;
        var encoder = new PngBitmapEncoder();
        var frame = BitmapFrame.Create(bitmap);
        encoder.Frames.Add(frame);
        using (var stream = new MemoryStream())
        {
            encoder.Save(stream);
            return System.Convert.ToBase64String(stream.ToArray());
        }
    }

}