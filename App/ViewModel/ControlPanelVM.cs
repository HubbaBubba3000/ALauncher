using System.Collections.ObjectModel;
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Core;
using System.Linq;
using ALauncher.View;
using System.Windows;
using System;

namespace ALauncher.ViewModel;

public sealed class ControlPanelVM : BaseVM, IDisposable
{
    private FolderManager folderManager;
    private Logger logger;
    private IconPackManager packManager;
    private WrapPanelVM wrapPanel;
    private CommandWrapper commandWrapper;
    private bool IsAddWindowOpen;
    private SettingsVM settingsVM;
    public ObservableCollection<Folder> Folders
    {
        get => folderManager.Folders;
        set
        {
            OnPropertyChanged("Folders");
        }
    }
    public Folder CurrentFolder
    {
        get => wrapPanel.CurrentFolder;
        set
        {
            GetIcons(value);
            wrapPanel.CurrentFolder = value;
            OnPropertyChanged("CurrentFolder");
        }
    }
    public void GetIcons(Folder folder)
    {
        foreach (Item item in folder.Items)
            item.Icon ??= packManager.GetIcon(item);
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
                packManager.SerializeIcons((FolderConfig)folderManager.GetConfig).ConfigureAwait(false);
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
                var buf = CurrentFolder;
                CurrentFolder = Folders.First();
                Folders.Remove(buf);
                folderManager.Save();
            });
        }
    }
    private void UpdateByStatus(LoggerCode code)
    {
        if (code == LoggerCode.FolderAsyncParseComplete)
        {
            Folders = null; // updating list
            CurrentFolder = Folders[0];
        }
    }

    public void Dispose()
    {
        logger.StatusChanged -= UpdateByStatus;
    }

    public ControlPanelVM(Logger bp, WrapPanelVM wp, IconPackManager ipm, FolderManager b, SettingsVM ss, CommandWrapper cw)
    {
        logger = bp;
        packManager = ipm;
        wrapPanel = wp;
        commandWrapper = cw;
        folderManager = b;
        settingsVM = ss;

        IsAddWindowOpen = false;
        logger.StatusChanged += UpdateByStatus;
    }
}