
using System.Windows.Input;
using ALauncher.Model;

namespace ALauncher.ViewModel;

public class SettingsVM : BaseVM {
    private SettingsManager settings;
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
            return new RelayCommand((obj) => {
                settings.SaveSettings(config);
                settings.SetWindowDefaultSize();
            });
        }
    }

    public SettingsVM(SettingsManager sm) {
        settings = sm;
        config = settings.GetSettings();
    }   
}