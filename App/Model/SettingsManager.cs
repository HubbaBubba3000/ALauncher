
using System;
using System.IO;
using System.Windows;
using System.Xaml;

namespace ALauncher.Model;

public class SettingsManager {
    SettingsConfig Settings;
    string Config;

    private string WorkFolder = $"C:/Users/{Environment.UserName}/Documents/ALauncher";

    public SettingsManager() {
        Config = WorkFolder+"/Settings.json";
        if (!File.Exists(Config)) {
            Settings = new SettingsConfig() {
                WindowWidth = 1280,
                WindowHeigth = 720,
                Lang = "EN",
                Net = false,
                AutoUpdate = false,
                Animations = false
            };
            JsonParser<SettingsConfig>.Save(Settings, Config);
            MessageBox.Show($"Settings config was created in {Config}");
            return;
        }

        Settings = JsonParser<SettingsConfig>.Parse(Config);
    }
    

    public void SaveSettings(SettingsConfig settings) {
        Settings = settings;
        JsonParser<SettingsConfig>.Save(Settings,Config);
    }
    public void SaveSettings() {
        JsonParser<SettingsConfig>.Save(Settings,Config);
    }

    public void SetWindowDefaultSize(Window window) {
        window.Width = Settings.WindowWidth;
        window.Height = Settings.WindowHeigth;
    }
    public void SaveWindowSize(Window window) {
        Settings.WindowWidth = window.Width;
        Settings.WindowHeigth = window.Height;
        SaveSettings();
    }
}