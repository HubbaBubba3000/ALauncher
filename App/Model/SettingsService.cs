using ALauncher.ViewModel;
using ALauncher.View;
using System;

namespace ALauncher.Model;

public class SettingsService : IService
{
    bool IsActivity;
    Func<SettingsVM> SettingsFactory;
    public SettingsService(Func<SettingsVM> settingsFactory) {
        SettingsFactory = settingsFactory;
        IsActivity = false;
    }
    public void Show()
    {
        IsActivity = true;
        SettingsVM settings = SettingsFactory.Invoke();
        SettingsWindow sw = new SettingsWindow() {
            DataContext = settings
            
        };
        sw.Show();
    }
}