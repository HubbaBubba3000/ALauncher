
using System.IO;
using System.Windows;
using ALauncher.Data;
namespace ALauncher.Model;

public sealed class SettingsManager : Manager {
    SettingsConfig Settings;
    Window mainWindow;
    string Config;

    public SettingsManager() {
        Config = WorkFolder+"/Settings.json";
        
        if (!File.Exists(Config)) {
            Settings = new SettingsConfig() {
                WindowWidth = 1280,
                WindowHeight = 720,
                Lang = Localisation.EN,
                AutoUpdate = false,
                Animations = false
            };
            JsonParser<SettingsConfig>.Save(Settings, Config);
            MessageBox.Show($"Settings config was created in {Config}");
            return;
        }

        Settings = JsonParser<SettingsConfig>.Parse(Config);
    }
    public void SetMainWindow(Window mw){
        mainWindow = mw;
    }
    public SettingsConfig GetSettings() => Settings;

    public void SaveSettings(SettingsConfig settings) {
        Settings = settings;
        JsonParser<SettingsConfig>.Save(Settings,Config);
    }
    public void SaveSettings() {
        JsonParser<SettingsConfig>.Save(Settings,Config);
    }

    public void SetWindowDefaultSize() {
        mainWindow.Width = Settings.WindowWidth;
        mainWindow.Height = Settings.WindowHeight;
    }
    public void SaveWindowSize() {
        Settings.WindowWidth = (int)mainWindow.Width;
        Settings.WindowHeight = (int)mainWindow.Height;
        SaveSettings();
    }
}