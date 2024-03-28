using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ALauncher.View;

public sealed class ItemControl : Button
{
#region Properties
    public static DependencyProperty IconProperty;
    public static DependencyProperty PathProperty;
    public static DependencyProperty AppNameProperty;
    public static DependencyProperty IsFavoriteProperty;
    static ItemControl()
    {
        IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ItemControl));
        PathProperty = DependencyProperty.Register("Path", typeof(string), typeof(ItemControl));
        AppNameProperty = DependencyProperty.Register("AppName", typeof(string), typeof(ItemControl));
        IsFavoriteProperty = DependencyProperty.Register("IsFavorite", typeof(bool), typeof(ItemControl));
    }
    public ImageSource Icon
    {
        get { return (ImageSource)base.GetValue(IconProperty); }
        set { base.SetValue(IconProperty, value); }
    }
    public string AppName
    {
        get { return (string)base.GetValue(AppNameProperty); }
        set { base.SetValue(AppNameProperty, value); }
    }
    public string Path
    {
        get { return (string)base.GetValue(PathProperty); }
        set { base.SetValue(PathProperty, value); }
    }
    public bool IsFavorite
    {
        get { return (bool)base.GetValue(IsFavoriteProperty); }
        set { base.SetValue(IsFavoriteProperty, value); }
    }
#endregion

    DrawingGroup backingStore;
    public ItemControl() 
    {
        
        DefaultStyleKey = typeof (ItemControl);
        backingStore = new DrawingGroup();
        CompositionTarget.Rendering += (obj, e) => Render();
    }

#region Render

    int thickness;
    bool StarVisibility;
    [Obsolete]
    public GlyphRun CreateGlyph(string text, Typeface typeface, double emSize, Point baselineOrigin)
    {
        GlyphTypeface glyphTypeface;

        if (!typeface.TryGetGlyphTypeface(out glyphTypeface))
        {
            throw new ArgumentException(string.Format(
                "{0}: no GlyphTypeface found", typeface.FontFamily));
        }

        var glyphIndices = new ushort[text.Length];
        var advanceWidths = new double[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            var glyphIndex = glyphTypeface.CharacterToGlyphMap[text[i]];
            glyphIndices[i] = glyphIndex;
            advanceWidths[i] = glyphTypeface.AdvanceWidths[glyphIndex] * emSize;
        }

        return new GlyphRun( glyphTypeface, 0, false, emSize, glyphIndices, baselineOrigin, advanceWidths, null, null, null,null, null, null);
    }
    protected override void OnRender(DrawingContext dc)
    {
        Render();
        base.OnRender(dc);
        dc.DrawDrawing(backingStore);
    }
    private void Render() {            
        var drawingContext = backingStore.Open();
        Render(drawingContext);
        drawingContext.Close();            
    }
    private void Render(DrawingContext dc) 
    {  
        int Fontsize = 12;
        Brush bg = new SolidColorBrush(Colors.Transparent) ;
        Brush b4 = (Brush)App.Current.FindResource("b4"); 
        Brush b2 = (Brush)App.Current.FindResource("b2");
        Typeface glyphTypeface = new("Arial");
        Point origin = new Point((Width-Fontsize*AppName.Length/2)/2, 53+Fontsize );
        GlyphRun glyph = CreateGlyph(AppName,glyphTypeface,Fontsize,origin);
        Rect bounds = new Rect(0, 0, this.Width, this.Height);
        Pen pen = new Pen(b4, thickness);
        dc.DrawRectangle(bg, pen, bounds);
        dc.DrawImage(Icon,new Rect(16,5,48,48));
        dc.DrawGlyphRun(b4, glyph);
        if (StarVisibility)
        {
            Geometry star = (Geometry)App.Current.FindResource("StarIcon");
            dc.DrawGeometry(IsFavorite ? b4 : b2, pen, star);
        }
    }
    protected override void OnMouseEnter(MouseEventArgs e)
    {
        thickness = 1;
        StarVisibility = true;
        base.OnMouseEnter(e);
    }
    protected override void OnMouseLeave(MouseEventArgs e)
    {
        StarVisibility = false;
        thickness = 0;
        base.OnMouseLeave(e);
    }

    #endregion

}