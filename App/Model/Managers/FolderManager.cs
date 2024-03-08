using ALauncher.Data;
using System.Windows;
using System;
using System.IO;
using System.Linq;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ALauncher.ViewModel;

namespace ALauncher.Model;

public sealed class FolderManager : Manager {
    public ObservableCollection<Folder> folders;
    public void UpdateFolders() {
        var config = new FolderConfig() {
            Folders = folders.ToArray()
        };
        JsonParser<FolderConfig>.Utf8SaveAsync(config, $"{WorkFolder}/Folders.json");
    }
    public void UpdateFolders(object? sender, NotifyCollectionChangedEventArgs e) {
        UpdateFolders();
    }
    public void CheckAndSetIcons(Folder folder) {
        if (folder == null || folder.Items == null) return;
        foreach (Item item in folder.Items) 
            if (item.Icon == null)
                item.Icon = IconExtractor.GetIcon(item.Path);
    }
    private async Task initFolder(string path) {
        var config = await JsonParser<FolderConfig>.Utf8ParseAsync(path);
        folders = new((Folder[])config.Folders.Clone());
        logger.SetStatusLog(201, "Async parsing complete");
        
    }
    private Logger logger;
    public FolderManager(Logger bp) {
        logger = bp;
        if (!Directory.Exists(WorkFolder))
            Directory.CreateDirectory(WorkFolder);
        string path = $"{WorkFolder}/Folders.json";
        folders = new();

        if (!File.Exists(path)) {
            
            UpdateFolders();
            MessageBox.Show($"Folders config was created in {path}");
            return;
        }
        else {
            logger.Status = "start async Init folder";
            initFolder(path).ConfigureAwait(false);
        }
        folders.CollectionChanged += UpdateFolders;
    }
}