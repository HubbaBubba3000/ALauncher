using ALauncher.Data;
using System.Windows;
using System;
using System.IO;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace ALauncher.Core;

public sealed class FolderManager : IManager, IDisposable
{
    private Logger logger;
    private FolderConfig config;
    public IConfig GetConfig
    {
        get => config;
    }
    public ObservableCollection<Folder> Folders
    {
        get => config.Folders;
    }
    private ObservableCollection<Item> fav; 
    public ObservableCollection<Item> Favorites 
    {
        get => fav;
    }
    public void SetFavorites() 
    {
        foreach (Folder folder in Folders)
            foreach (Item item in folder.Items)
                if (item.IsFavorite)
                    Favorites.Add(item);
    
    }
    public void UpdateFolders(object? sender, NotifyCollectionChangedEventArgs e)
    {
        Save();
    }
    private async Task InitFolder(string path)
    {
        config = await JsonParser<FolderConfig>.ParseAsync(path);
        logger.SetStatusLog(201, "Async parsing complete");
    }
    public void Save()
    {
        JsonSaver<FolderConfig>.SaveAsync(config, $"{ManagerHelper.WorkFolder}/Folders.json");
    }
    public void Load(string path)
    {
        config = new();
        if (!File.Exists(path))
        {
            MessageBox.Show($"Folders config was created in {path}");
            return;
        }
        else
        {
            logger.SetStatusLog(0, "Start Async parse");
            InitFolder(path).ConfigureAwait(false);

        }
        Folders.CollectionChanged += UpdateFolders;
    }

    public void Dispose()
    {
        Folders.CollectionChanged -= UpdateFolders;
    }

    public FolderManager(Logger bp)
    {
        logger = bp;
        fav = new();
        if (!Directory.Exists(ManagerHelper.WorkFolder))
            Directory.CreateDirectory(ManagerHelper.WorkFolder);
        Load($"{ManagerHelper.WorkFolder}/Folders.json");

    }
}
