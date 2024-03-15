using System.Text;
using ALauncher.Core;
using DryIoc.ImTools;

namespace ALauncher.ViewModel;

public sealed class BottomPanelVM : BaseVM
{
    private Logger logger;
    public string Version
    {
        get
        {
            StringBuilder ver = new("v0.1.4 - ");
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
    public BottomPanelVM(Logger logger)
    {
        this.logger = logger;
        logger.StatusChanged += (code) => { Status = ""; };
    }
}