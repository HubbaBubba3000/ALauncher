
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace ALauncher;

public sealed class SharedResourceDictionary : ResourceDictionary
{
    private static Dictionary<Uri, ResourceDictionary> sharedDictionaries =
        new Dictionary<Uri, ResourceDictionary>();

    private static bool isInDesignerMode;
    private Uri sourceUri;

    static SharedResourceDictionary()
    {
        isInDesignerMode = (bool)DesignerProperties.IsInDesignModeProperty
                            .GetMetadata(typeof(DependencyObject)).DefaultValue;
    }

    public static Dictionary<Uri, ResourceDictionary> SharedDictionaries
    {
        get => sharedDictionaries;
    }
    public new Uri Source
    {
        get => this.sourceUri;
        set
        {
            this.sourceUri = value;

            if (!sharedDictionaries.ContainsKey(value) || isInDesignerMode)
            {
                base.Source = value;
                if (!isInDesignerMode)
                    sharedDictionaries.Add(value, this);
            }
            else
                this.MergedDictionaries.Add(sharedDictionaries[value]);
        }
    }
}