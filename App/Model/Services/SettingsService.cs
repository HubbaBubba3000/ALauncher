using ALauncher.ViewModel;
using ALauncher.View;
using System;
using ALauncher.Data;

namespace ALauncher.Model;

public sealed class SettingsService : IService
{
    SettingsVM SettingsFactory;
    SettingsWindow settingsWindow;
    public SettingsService(SettingsVM settingsFactory) {
        SettingsFactory = settingsFactory;
    }

    public bool Show()
    {
        settingsWindow = new SettingsWindow() {
            DataContext = SettingsFactory
        };
        settingsWindow.Show();
        return true;
    }
}