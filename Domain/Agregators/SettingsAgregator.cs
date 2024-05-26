using ALauncher.Abstractions.Domain;

namespace ALauncher.Domain.Agregators;

public class SettingsAgregator : IAgregator 
{
    public SettingsAgregator() { }
    public int WindowWidth {get; set;} = 800;
    public int WindowHeight {get; set;} = 600;
    public int ControlPanelWidth {get; set;} = 200;
    /// <summary>
    /// Change Language
    /// </summary>
    //public Localisation Lang {get; set;} = Localisation.EN;
    /// <summary>
    /// Background Image Path
    /// </summary>
    public string BackgroundPath {get; set;} = string.Empty;
    /// <summary>
    /// Start only tray Icon (without init mainwindow)
    /// </summary>
    public bool StartMinimize {get; set;} = false;

}
