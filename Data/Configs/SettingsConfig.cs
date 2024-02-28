
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ALauncher.Data;
 [JsonConverter(typeof(JsonStringEnumConverter))]
public enum Localisation {
    [EnumMember(Value = "EN")]
    EN,
    [EnumMember(Value = "ES")]
    ES,
    [EnumMember(Value = "DE")]
    DE,
    [EnumMember(Value = "RU")]
    RU

}

public class SettingsConfig : IConfig, IDisposable {

    public int WindowWidth {get; set;}
    public int WindowHeight {get; set;}
    /// <summary>
    /// Change Language (NOT IMPLEMENTED)
    /// </summary>
    public Localisation Lang {get; set;}
    /// <summary>
    /// auto Updating app
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