
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace ALauncher.View;

public class ResizeAdorner : Adorner {
    VisualCollection AdornerVisual;
    Thumb thumb;
    public ResizeAdorner(UIElement adornedElement) : base(adornedElement) {
        AdornerVisual = new VisualCollection(this);

        thumb = new Thumb() {
            
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECB365")),
            Width=5, Height=20
        };
        thumb.DragDelta += OnDragDelta;

        AdornerVisual.Add(thumb);

    }
    private void OnDragDelta(object sender, DragDeltaEventArgs e) {
        var Element = (FrameworkElement)AdornedElement;
        Element.Width = Element.Width + e.HorizontalChange < 0 ? 0 : Element.Width + e.HorizontalChange;
    }

    protected override Visual GetVisualChild(int index)
    {
        return AdornerVisual[index];
    }
    protected override int VisualChildrenCount => AdornerVisual.Count;

    protected override Size ArrangeOverride(Size finalSize)
    {
        thumb.Arrange(new(AdornedElement.DesiredSize.Width,AdornedElement.DesiredSize.Height/2 ,5,20));
        return base.ArrangeOverride(finalSize);
    }
}