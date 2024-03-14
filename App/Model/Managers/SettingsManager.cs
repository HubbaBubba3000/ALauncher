
using System;
using System.IO;
using System.Windows;
using ALauncher.Data;
namespace ALauncher.Model;

public sealed class SettingsManager : Manager {
    SettingsConfig Settings;
    Window mainWindow;
    string Config;
    public event EventHandler SettingsChanged;
    private delegate void SettingsChangedHandler();

    public SettingsManager() {
        Config = WorkFolder+"/Settings.json";
        
        SettingsChanged += (obj,e) => {SetWindowSize();};
        if (!File.Exists(Config)) {
            Settings = new SettingsConfig() {
                WindowWidth = 1280,
                WindowHeight = 720,
                ControlPanelWidth = 200,
                Lang = Localisation.EN,
                AutoUpdate = false,
                BackgroundPath = ""
            };
            SaveSettings(Settings);
            MessageBox.Show($"Settings config was created in {Config}");
            return;
        }
        Settings = JsonParser<SettingsConfig>.Parse(Config);
        ((App)App.Current).SetLanguage(Settings.Lang);
    }
    public void SetMainWindow(Window mw){
        mainWindow = mw;
    }
    public void SetPanelWidth(int width) {
        Settings.ControlPanelWidth = width;
    }
    public SettingsConfig GetSettings() => Settings;
    public void SaveSettings(SettingsConfig settings) {
        Settings = settings;
        ((App)App.Current).SetLanguage(settings.Lang);
        JsonParser<SettingsConfig>.Save(Settings,Config);
        SettingsChanged?.Invoke(null, new());
    }
    public void SaveSettings() {
        JsonParser<SettingsConfig>.Save(Settings,Config);
        SettingsChanged?.Invoke(null, new());
    }

    public void SetWindowSize() {
        mainWindow.Width = Settings.WindowWidth;
        mainWindow.Height = Settings.WindowHeight;
    }
    public void SaveWindowSize() {
        Settings.WindowWidth = (int)mainWindow.Width;
        Settings.WindowHeight = (int)mainWindow.Height;
    }
}