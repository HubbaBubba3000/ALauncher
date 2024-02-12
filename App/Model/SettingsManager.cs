
using System;
using System.IO;
using System.Windows;
using System.Xaml;

namespace ALauncher.Model;

public class SettingsManager {
    SettingsConfig Settings;
    string Config;

    private string WorkFolder = $"configs/";

    public SettingsManager() {
        Config = WorkFolder+"/Settings.json";
        
        if (!File.Exists(Config)) {
            Settings = new SettingsConfig() {
                WindowWidth = 1280,
                WindowHeight = 720,
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
    public SettingsConfig GetSettings() => Settings;

    public void SaveSettings(SettingsConfig settings) {
        Settings = settings;
        JsonParser<SettingsConfig>.Save(Settings,Config);
    }
    public void SaveSettings() {
        JsonParser<SettingsConfig>.Save(Settings,Config);
    }

    public void SetWindowDefaultSize(Window window) {
        window.Width = Settings.WindowWidth;
        window.Height = Settings.WindowHeight;
    }
    public void SaveWindowSize(Window window) {
        Settings.WindowWidth = (int)window.Width;
        Settings.WindowHeight = (int)window.Height;
        SaveSettings();
    }
}