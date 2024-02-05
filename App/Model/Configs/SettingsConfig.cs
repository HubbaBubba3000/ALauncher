
namespace ALauncher.Model;

public class SettingsConfig : IConfig {

    int WindowWidth {get; set;}
    int WindowHeigth {get; set;}

    string Lang {get; set;}
    /// <summary>
    /// on/off network module
    /// </summary>
    bool Net {get;set;}
    /// <summary>
    /// auto Updating app, if net is false it doesnt work 
    /// </summary>
    bool AutoUpdate {get;set;}
    bool Anomation {get; set;}

}