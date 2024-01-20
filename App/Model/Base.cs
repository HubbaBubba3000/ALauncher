using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using ALauncher.Data;
using ALauncher.ViewModel;
using System.Windows;

namespace ALauncher.Model;

public class Base {
    public List<Folder> folders;
    public ImageSource GetIcon(string path) {
        Icon? icon = Icon.ExtractAssociatedIcon(path);
        return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle, 
                new Int32Rect(0,0,icon.Width,icon.Height),
                BitmapSizeOptions.FromEmptyOptions());
    }

    public Base() {
        folders = new(1);
        using (var config = JsonParser<FolderConfig>.Parse("Configs/Folders.json")) {
            folders.AddRange((Folder[])config.Folders.Clone());
        }
        foreach (Folder folder in folders) 
            foreach (Item item in folder.Items) 
                item.Icon = GetIcon(item.Path);
    }
}
