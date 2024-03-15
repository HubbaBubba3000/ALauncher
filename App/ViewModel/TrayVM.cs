
using System.Windows;
using System.Windows.Input;
using ALauncher.Core;

namespace ALauncher.ViewModel;

public class TrayVM : BaseVM
{
    private CommandWrapper commands;
    private MainWindow main;
    private Logger logger;
    public TrayVM(CommandWrapper cw, MainWindow mw, Logger bp)
    {
        commands = cw;
        logger = bp;
        logger.StatusChanged += (code) =>
        {
            if (code == LoggerCode.ProcessStarted)
                CloseWindowCommand.Execute(null);
            else if (code == LoggerCode.ProcessClosed)
                ShowWindowCommand.Execute(null);
        };
        main = mw;
    }
    public ICommand ShowWindowCommand
    {
        get => commands.GetCommand(
            (obj) =>
            {
                Application.Current.MainWindow = main;
                Application.Current.MainWindow.Show();
            },
            (obj) => { return Application.Current.MainWindow == null; }
        );
    }
    public ICommand CloseWindowCommand
    {
        get => commands.GetCommand(
            (obj) =>
            {
                Application.Current.MainWindow.Close();
            },
            (obj) => { return Application.Current.MainWindow == null; }
        );
    }
    public ICommand ExitApplicationCommand
    {
        get => commands.GetCommand((obj) => Application.Current.Shutdown());
    }
}