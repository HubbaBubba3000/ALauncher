
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using ALauncher.Domain;
using ALauncher.Entities.Containers;

namespace ALauncher.WPF.Controls.Tray;

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
        logger.StatusChanged += StatusChanged;
        // update List
        folderManager.Folders.CollectionChanged += (obj, e) => {Favorites = null; };
    }
    public string Version 
    {
        get => $"ALauncher v.{Assembly.GetExecutingAssembly().GetName().Version}";
    }
    public void StatusChanged(LoggerCode code) 
    {
        switch (code) 
        {
            case LoggerCode.ProcessStarted :
                CloseWindowCommand.Execute(null);
                break;
            case LoggerCode.ProcessClosed :
                ShowWindowCommand.Execute(null);
                break;
            case LoggerCode.FolderAsyncParseComplete :
                folderManager.SetFavorites(); 
                break;
        }
    }
    public ObservableCollection<Item> Favorites
    {
        get => folderManager.Favorites;
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
                if (obj == null) return;
                var item = Favorites.Single(item => item.Path == (string)obj);//obj is path
                ProcessWorker process = new(item);
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