using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using ALauncher.Data;
using ALauncher.ViewModel;
using System.Windows;
using System.Diagnostics;
using System.IO;

namespace ALauncher.Model;

public class Base {
    public List<Folder> folders;
    public void RunItem(string path) {
        ProcessStartInfo processInfo = new ProcessStartInfo(path) {
            WorkingDirectory = Path.GetDirectoryName(path)
        };
        Process.Start(processInfo);
    }
    public void UpdateFolers() {
        var config = new FolderConfig() {
            Folders = folders.ToArray()
        };
        JsonParser<FolderConfig>.Save(config, "Configs/Folders.json");
    }
    public Base() {
        folders = new(1);
        using (var config = JsonParser<FolderConfig>.Parse("Configs/Folders.json")) 
            folders.AddRange((Folder[])config.Folders.Clone());

        foreach (Folder folder in folders) 
            foreach (Item item in folder.Items) 
                item.Icon = IconExtractor.GetIcon(item.Path);
    }
}
