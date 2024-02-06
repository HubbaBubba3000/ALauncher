
using System.Windows;

namespace ALauncher.Model;

public class SettingsManager {
    SettingsConfig Settings;

    string Config;
    public SettingsManager(string config) {
        Config = config;
        Settings = JsonParser<SettingsConfig>.Parse(config);
    }

    public void SaveSettings(SettingsConfig settings) {
        Settings = settings;
        JsonParser<SettingsConfig>.Save(Settings,Config);
    }

    public void SetWindowDefaultSize(Window window) {
        window.Width = Settings.WindowWidth;
        window.Height = Settings.WindowHeigth;
    }
}