using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using ALauncher.WPF.Common;
using ALauncher.Domain.Agregators;
using ALauncher.Domain.ConfigRepositories;

namespace ALauncher.WPF.Controls.Settings;

public sealed class SettingsVM : BaseVM
{
    private SettingsAgregator Settings;
    private ConfigSaveRepository SettingsSaver;
    private CommandWrapper commandWrapper;
    public int WindowWidth
    {
        get => Settings.WindowWidth;
        set
        {
            Settings.WindowWidth = value;
            OnPropertyChanged("WindowWidth");
        }
    }
    public int WindowHeight
    {
        get => Settings.WindowHeight;
        set
        {
            Settings.WindowHeight = value;
            OnPropertyChanged("WindowHeight");
        }
    }
    public bool StartMinimize
    {
        get => Settings.StartMinimize;
        set
        {
            Settings.StartMinimize = value;
            OnPropertyChanged("StartMinimize");
        }
    }
    public string Background
    {
        get => Settings.BackgroundPath;
        set
        {
            Settings.BackgroundPath = value;
            OnPropertyChanged("Background");
        }
    }
    public ICommand Save
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                if (!string.IsNullOrEmpty(Background) && !Path.IsPathRooted(Background))
                {
                    MessageBox.Show("Background Path is not valid");
                    return;

                }
                SettingsSaver.Save(Settings,"Settings");
            });
        }
    }
    public ICommand Browse
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                OpenFileDialog ofd = new()
                {
                    Filter = "Images |*.jpg;*.jpeg;*.png;*.bmp;",
                    Multiselect = false
                };
                if (ofd.ShowDialog() == true)
                {
                    Background = ofd.FileName;
                }
            });
        }
    }

    public SettingsVM(ConfigSaveRepository csr, AgregatorFactory af, CommandWrapper cw)
    {
        SettingsSaver = csr;
        commandWrapper = cw;
        Settings = af.GetSettings();
    }
}