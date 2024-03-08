using ALauncher.Model;

namespace ALauncher.ViewModel;

public sealed class BottomPanelVM : BaseVM {
   private Logger logger;
    public string Version {
        get {
            string ver ="v0.1.2 - ";
            #if DEBUG
                ver += "DEBUG";
            #else 
                ver += "Release";
            #endif
            return ver;
        }
    }
    public string Status {
        get => logger.Status;
    }
   
    public BottomPanelVM(Logger logger) {
        this.logger = logger;
    }
}