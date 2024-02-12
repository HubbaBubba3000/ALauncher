
using System;

namespace ALauncher.Model;

public class SettingsConfig : IConfig, IDisposable {

    public int WindowWidth {get; set;}
    public int WindowHeight {get; set;}
    /// <summary>
    /// Change Language (NOT IMPLEMENTED)
    /// </summary>
    public string Lang {get; set;}
    /// <summary>
    /// on/off network module
    /// </summary>
    public bool Net {get;set;}
    /// <summary>
    /// auto Updating app, if net is false it doesnt work 
    /// </summary>
    public bool AutoUpdate {get;set;}
    /// <summary>
    /// On/Off animations (NOT IMPLEMENTED)
    /// </summary>
    public bool Animations {get; set;}

    public void Dispose()
    {
    }
}