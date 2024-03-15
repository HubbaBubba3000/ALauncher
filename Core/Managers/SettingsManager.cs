
using System.IO;
using System.Windows;
using ALauncher.Data;
namespace ALauncher.Core;

public sealed class SettingsManager : IManager
{
    SettingsConfig Settings;
    Window mainWindow;
    string Config;
    public IConfig GetConfig => Settings;
    public event SettingsChangedHandler SettingsChanged;
    public delegate void SettingsChangedHandler();

    public SettingsManager()
    {
        Config = ManagerHelper.WorkFolder + "/Settings.json";
        Load(Config);
        SettingsChanged += SetWindowSize;
    }
    public void SetMainWindow(Window mw)
    {
        mainWindow = mw;
    }
    public void SetPanelWidth(int width)
    {
        Settings.ControlPanelWidth = width;
    }
    public SettingsConfig GetSettings() => Settings;
    public void SetWindowSize()
    {
        mainWindow.Width = Settings.WindowWidth;
        mainWindow.Height = Settings.WindowHeight;
    }
    public void SaveWindowSize()
    {
        Settings.WindowWidth = (int)mainWindow.Width;
        Settings.WindowHeight = (int)mainWindow.Height;
    }

    public void Save()
    {
        JsonSaver<SettingsConfig>.Save(Settings, Config);
        SettingsChanged?.Invoke();
    }
    public void Save(SettingsConfig settings)
    {
        JsonSaver<SettingsConfig>.Save(settings, Config);
        SettingsChanged?.Invoke();
    }
    public void Load(string path)
    {
        if (File.Exists(path))
        {
            Settings = JsonParser<SettingsConfig>.Parse(path);
        }
        else
        {
            Settings = new SettingsConfig();
            Save();
            MessageBox.Show($"Settings config was created in {Config}");
        }
    }
    public void FirstInit()
    {
        SettingsChanged?.Invoke();
    }
}