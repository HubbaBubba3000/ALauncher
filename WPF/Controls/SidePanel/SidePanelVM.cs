using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System;
using ALauncher.Abstractions.Entities;
using ALauncher.Entities.Containers;
using ALauncher.Domain.Agregators;
using ALauncher.Domain.Logging;
using ALauncher.WPF.Common;
using ALauncher.WPF.Model.Converters;
using ALauncher.Domain.CacheRepositories;
using ALauncher.WPF.Model.Services;

namespace ALauncher.WPF.Controls.SidePanel;

public sealed class SidePanelVM : BaseVM, IDisposable
{
    StorageAgregator Storage;
    Logger logger;
    Base64ToImageConverter Base64ToImage;
    IconExtractorService IconExtractor;
    CacheSaveRepository CacheSaver;
    IconCacheAgregator IconCache;
    CommandWrapper commandWrapper;
    public ObservableCollection<IFolder> Folders
    {
        get => Storage.Folders;
        set
        {
            OnPropertyChanged("Folders");
        }
    }
    public ICommand AddFolder
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                AddictionFolder window = new();
                if (window.ShowDialog() == true)
                {
                    Folders.Add(window.GetFolder);
                }
                IsAddWindowOpen = false;
            });
        }
    }
    public ICommand OpenSettings
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                var settingsWindow = new SettingsWindow()
                {
                    DataContext = settingsVM
                };
                settingsWindow.ShowDialog();
            });
        }
    }
    public ICommand EditFolder
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                AddictionFolder window = new(CurrentFolder);
                if (window.ShowDialog() == true)
                {
                    int i = Folders.IndexOf(CurrentFolder);
                    Folders.RemoveAt(i);
                    Folders.Insert(i, window.GetFolder);
                    CurrentFolder = Folders.ElementAt(i);
                }
                IsAddWindowOpen = false;
            });
        }
    }
    public ICommand DeleteFolder
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                if (Folders.Count == 0) 
                    return;

                if (Folders.Count == 1) 
                {
                    Folders.Remove(CurrentFolder);
                    Folders.Add(new Folder());
                    CurrentFolder = Folders.First();
                }
                else 
                {
                    var buf = CurrentFolder;
                    CurrentFolder = Folders.First();
                    Folders.Remove(buf);
                }
                folderManager.Save();
            });
        }
    }

    public void Dispose()
    {
        CacheSaver.SaveAsync(IconCache,"Icons").ConfigureAwait(false);
        logger.StatusChanged -= UpdateByStatus;
    }

    public SidePanelVM(
        Logger bp,
        CacheSaveRepository csr,
        IconExtractorService ies,
        Base64ToImageConverter b2ic, 
        AgregatorFactory af, 
        CommandWrapper cw)
    {
        logger = bp;
        IconCache = af.GetIconCache();
        IconExtractor = ies;
        Base64ToImage = b2ic;
        commandWrapper = cw;
        Storage = af.GetStorage();
        CacheSaver = csr;
        logger.StatusChanged += UpdateByStatus;
    }
}