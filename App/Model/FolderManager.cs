using ALauncher.Data;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace ALauncher.Model;

public class FolderManager : Manager {
    public ObservableCollection<Folder> folders;
    public void RunItem(string path) {
        ProcessStartInfo processInfo = new ProcessStartInfo(path) {
            WorkingDirectory = Path.GetDirectoryName(path)
        };
        Process.Start(processInfo);
    }
    public void UpdateFolders() {
        var config = new FolderConfig() {
            Folders = folders.ToArray()
        };
        JsonParser<FolderConfig>.Save(config, $"{WorkFolder}/Folders.json");
    }
    public void UpdateFolders(object? sender, NotifyCollectionChangedEventArgs e) {
        UpdateFolders();
    }
    public void initFolder(string path) {
         using (var config = JsonParser<FolderConfig>.Parse(path))
            folders = new((Folder[])config.Folders.Clone()) ;

        foreach (Folder folder in folders ) 
            foreach (Item item in folder.Items) 
                IconExtractor.SetIcon(item);
    }
    public FolderManager() {
        
        string path = $"{WorkFolder}/Folders.json";

        if (!File.Exists(path)) {
            folders = new();
            UpdateFolders();
            MessageBox.Show($"Folders config was created in {path}");
            return;
        }
        else {
            initFolder(path);
        }

        folders.CollectionChanged += UpdateFolders;
    }
}
