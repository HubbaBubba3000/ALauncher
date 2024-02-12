
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
    public bool Network {
        get {
            return config.Net;
        }
        set {
            config.Net = value;
            OnPropertyChanged("Network");
        }
    }
    public ICommand Save {
        get {
            return new RelayCommand((obj) => {
                settings.SaveSettings(config);
            });
        }
    }

    public SettingsVM(SettingsManager sm) {
        settings = sm;
        config = settings.GetSettings();
    }   
}