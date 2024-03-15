using ALauncher.Core;
using DryIoc.ImTools;

namespace ALauncher.ViewModel;

public sealed class BottomPanelVM : BaseVM {
    private Logger logger;
    public string Version {
        get {
            string ver ="v0.1.4 - ";
            #if DEBUG
                ver += "DEBUG";
            #else 
                ver += "Release";
            #endif
            return ver;
        }
    }
    public string Status {
        get => logger.Status.ToString();
        set {
            //logger.Status = value;
            OnPropertyChanged("Status");
        }
    }
    public BottomPanelVM(Logger logger) {
        this.logger = logger;
        logger.StatusChanged += (code) => { Status = "";};
    }
}