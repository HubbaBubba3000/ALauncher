using System.Collections.Generic;
using ALauncher.Data;
using ALauncher.ViewModel;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Specialized;
using System;

namespace ALauncher.Model;

public class FolderManager {
    private string WorkFolder = $"C:/Users/{Environment.UserName}/Documents/ALauncher";
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
    public FolderManager() {
       using (var config = JsonParser<FolderConfig>.Parse($"{WorkFolder}/Folders.json"))
            folders = new ((Folder[])config.Folders.Clone());
        folders.CollectionChanged += UpdateFolders;
        foreach (Folder folder in folders) 
            foreach (Item item in folder.Items) 
                item.Icon = IconExtractor.GetIcon(item.Path);
    }
}
