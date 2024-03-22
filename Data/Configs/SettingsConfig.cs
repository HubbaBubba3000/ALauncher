
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ALauncher.Data;
 [JsonConverter(typeof(JsonStringEnumConverter))]
public enum Localisation {
    [EnumMember(Value = "EN")]
    EN,
    [EnumMember(Value = "RU")]
    RU
}
public sealed class SettingsConfig : IConfig, IDisposable {

    public int WindowWidth {get; set;} = 800;
    public int WindowHeight {get; set;} = 600;
    public int ControlPanelWidth {get; set;} = 200;
    /// <summary>
    /// Change Language
    /// </summary>
    public Localisation Lang {get; set;} = Localisation.EN;
    /// <summary>
    /// Background Image Path
    /// </summary>
    public string BackgroundPath {get; set;} = string.Empty;
    /// <summary>
    /// Start only tray Icon (without init mainwindow)
    /// </summary>
    public bool StartMinimize {get; set;} = false;

    public void Dispose() {}

    public SettingsConfig() {}
}