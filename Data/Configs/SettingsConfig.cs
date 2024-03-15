
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
public sealed class SettingsConfig : IConfig, IDisposable {

    public int WindowWidth {get; set;} = 800;
    public int WindowHeight {get; set;} = 600;
    public int ControlPanelWidth {get; set;} = 200;
    /// <summary>
    /// Change Language (NOT IMPLEMENTED)
    /// </summary>
    public Localisation Lang {get; set;} = Localisation.EN;
    /// <summary>
    /// auto Updating app
    /// </summary>
    public bool AutoUpdate {get;set;} = false;
    /// <summary>
    /// Background Image Path
    /// </summary>
    public string BackgroundPath {get; set;} = string.Empty;

    public void Dispose() {}

    public SettingsConfig() {}
}