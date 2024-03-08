
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Model;

namespace ALauncher.ViewModel;

public sealed class SettingsVM : BaseVM {
    private SettingsManager settings;
    private CommandWrapper commandWrapper;
    private SettingsConfig config;
    public int WindowWidth {
        get {
            return config.WindowWidth;
        }
        set {
            config.WindowWidth = value;
            OnPropertyChanged("WindowWidth");
        }
    }
    public int WindowHeight {
        get {
            return config.WindowHeight;
        }
        set {
            config.WindowHeight = value;
            OnPropertyChanged("WindowHeight");
        }
    }
    public Localisation Lang {
        get {
            return config.Lang;
        }
        set {
            config.Lang = value;
            OnPropertyChanged("Lang");
        }
    }
    public bool AutoUpdate {
        get {
            return config.AutoUpdate;
        }
        set {
            config.AutoUpdate = value;
            OnPropertyChanged("AutoUpdate");
        }
    }
    public ICommand Save {
        get {
            return commandWrapper.GetCommand((obj) => {
                settings.SaveSettings(config);
                settings.SetWindowDefaultSize();
            });
        }
    }

    public SettingsVM(SettingsManager sm, CommandWrapper cw) {
        settings = sm;
        commandWrapper = cw;
        config = settings.GetSettings();
    }   
}