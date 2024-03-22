
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ALauncher.Core;
using ALauncher.Data;

namespace ALauncher.ViewModel;

public class TrayVM : BaseVM
{
    private CommandWrapper commands;
    private Logger logger;
    private FolderManager folderManager;
    public TrayVM(CommandWrapper cw, Logger bp, FolderManager fm)
    {
        folderManager = fm;
        commands = cw;
        logger = bp;
        logger.StatusChanged += (code) =>
        {
            if (code == LoggerCode.ProcessStarted)
                CloseWindowCommand.Execute(null);
            else if (code == LoggerCode.ProcessClosed)
                ShowWindowCommand.Execute(null);
        };
        folderManager.Folders.CollectionChanged += (obj, e) => {Favorites = null;};
    }
    public ObservableCollection<Item> Favorites
    {
        get
        {
            ObservableCollection<Item> fav = new();
            foreach (Folder folder in folderManager.Folders)
                foreach (Item item in folder.Items)
                    if (item.IsFavorite)
                        fav.Add(item);
            return fav;
        }
        set
        {
            OnPropertyChanged("Favorite");
        }
    }
    public ICommand ShowWindowCommand
    {
        get => commands.GetCommand(
            (obj) =>
            {
                ((App)Application.Current).InitMainWindow();
            },
            (obj) => { return App.Current.MainWindow == null; }
        );
    }
    public ICommand Run
    {
        get
        {
            return commands.GetCommand((obj) =>
            {
                var item = Favorites.Single(item => item.Path == (string)obj);//obj is path
                ProcessWorker process = new(item);
                logger.SetStatusLog(LoggerCode.ProcessStarted, $"process {process.ProcessName} Started");
                process.SetExitEvent((_obj, e) =>
                {
                    logger.SetStatusLog(LoggerCode.ProcessClosed, $"process {process.ProcessName} Closed ");
                    process.Dispose();
                });
                process.RunProcess();
            });
        }
    }
    public ICommand CloseWindowCommand
    {
        get => commands.GetCommand(
            (obj) =>
            {
                ((App)App.Current).DisposeMainWindow();
            },
            (obj) => { return App.Current.MainWindow != null; }
        );
    }
    public ICommand ExitApplicationCommand
    {
        get => commands.GetCommand((obj) => App.Current.Shutdown());
    }
}