
using System.IO;
using System.Windows;
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Core;
using Microsoft.Win32;

namespace ALauncher.ViewModel;

public sealed class SettingsVM : BaseVM
{
    private SettingsManager settings;
    private CommandWrapper commandWrapper;
    private SettingsConfig config;
    public int WindowWidth
    {
        get => config.WindowWidth;
        set
        {
            config.WindowWidth = value;
            OnPropertyChanged("WindowWidth");
        }
    }
    public int WindowHeight
    {
        get => config.WindowHeight;
        set
        {
            config.WindowHeight = value;
            OnPropertyChanged("WindowHeight");
        }
    }
    public Localisation Lang
    {
        get => config.Lang;
        set
        {
            config.Lang = value;
            OnPropertyChanged("Lang");
        }
    }
    public bool StartMinimize
    {
        get => config.StartMinimize;
        set
        {
            config.StartMinimize = value;
            OnPropertyChanged("StartMinimize");
        }
    }
    public string Background
    {
        get => config.BackgroundPath;
        set
        {
            config.BackgroundPath = value;
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
                settings.Save(config);
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

    public SettingsVM(SettingsManager sm, CommandWrapper cw)
    {
        settings = sm;
        commandWrapper = cw;
        config = settings.GetSettings();
    }
}