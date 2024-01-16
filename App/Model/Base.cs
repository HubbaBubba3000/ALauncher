using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using ALauncher.Data;
using System.Windows;

namespace ALauncher.Model;

public class Base {
    public Folder[] folders;
    public ImageSource GetIcon(string path) {
        Icon? icon = Icon.ExtractAssociatedIcon(path);
        return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle, 
                new Int32Rect(0,0,icon.Width,icon.Height),
                BitmapSizeOptions.FromEmptyOptions());
    }

    public Base() {
        using (var config = JsonParser<FolderConfig>.Parse("Configs/Folders.json")) {
            folders = (Folder[])config.Folders.Clone();
        }
        foreach (Folder folder in folders) 
            foreach (Item item in folder.Items) 
                item.Icon = GetIcon(item.Path);
    }
}
