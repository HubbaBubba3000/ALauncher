using System;
using System.Reflection;
using System.Text;
using ALauncher.Core;

namespace ALauncher.ViewModel;

public sealed class BottomPanelVM : BaseVM, IDisposable
{
    private Logger logger;
    public string Version
    {
        get
        {
            StringBuilder ver = new($"v.{Assembly.GetExecutingAssembly().GetName().Version} - " );
#if DEBUG
            ver.Append("DEBUG");
#else
            ver.Append("Release");
#endif
            return ver.ToString();
        }
    }
    public string Status
    {
        get => logger.Status.ToString();
        set
        {
            //logger.Status = value;
            OnPropertyChanged("Status");
        }
    }
    private void UpdateStatus(LoggerCode code) {
        Status = "";
    }

    public void Dispose()
    {
        logger.StatusChanged -= UpdateStatus;
    }

    public BottomPanelVM(Logger logger)
    {
        this.logger = logger;
        logger.StatusChanged += UpdateStatus;
    }
}