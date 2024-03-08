using ALauncher.ViewModel;
using ALauncher.View;
using System;
using ALauncher.Data;

namespace ALauncher.Model;

public sealed class SettingsService : IService
{
    Func<SettingsVM> SettingsFactory;
    SettingsWindow settingsWindow;
    public SettingsService(Func<SettingsVM> settingsFactory) {
        SettingsFactory = settingsFactory;
    }

    public bool Show()
    {
        settingsWindow = new SettingsWindow() {
            DataContext = SettingsFactory.Invoke()
        };
        settingsWindow.Show();
        return true;
    }
}