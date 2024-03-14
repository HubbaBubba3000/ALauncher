
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ALauncher.Data;
 [JsonConverter(typeof(JsonStringEnumConverter))]
public enum Localisation {
    None,
    [EnumMember(Value = "EN")]
    EN,
    [EnumMember(Value = "RU")]
    RU
}
public struct SettingsConfig : IConfig, IDisposable {

    public int WindowWidth {get; set;}
    public int WindowHeight {get; set;}
    public int ControlPanelWidth {get; set;}
    /// <summary>
    /// Change Language (NOT IMPLEMENTED)
    /// </summary>
    public Localisation Lang {get; set;}
    /// <summary>
    /// auto Updating app
    /// </summary>
    public bool AutoUpdate {get;set;}
    /// <summary>
    /// Background Image Path
    /// </summary>
    public string BackgroundPath {get; set;}

    public void Dispose()
    {
    }
}