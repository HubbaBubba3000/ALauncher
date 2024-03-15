using ALauncher.ViewModel;
using ALauncher.View;
using ALauncher.Data;

namespace ALauncher.Model;

public sealed class SettingsFactory : IWindowFactory
{
    SettingsVM settingsVM;
    public SettingsFactory(SettingsVM settingsFactory)
    {
        settingsVM = settingsFactory;
    }

    public bool Show()
    {
        var settingsWindow = new SettingsWindow()
        {
            DataContext = settingsVM
        };
        settingsWindow.ShowDialog();
        return true;
    }
}