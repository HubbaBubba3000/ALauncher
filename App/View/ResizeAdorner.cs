
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace ALauncher.View;

public class ResizeAdorner : Adorner {
    VisualCollection AdornerVisual;
    Thumb thumb;
    public ResizeAdorner(UIElement adornedElement, Thumb t) : base(adornedElement) {
        AdornerVisual = new VisualCollection(this);

        thumb = t;
        
        thumb.DragDelta += OnDragDelta;

        AdornerVisual.Add(thumb);

    }
    private void OnDragDelta(object sender, DragDeltaEventArgs e) {
        var Element = (FrameworkElement)AdornedElement;
        if (Element.Width + e.HorizontalChange < Application.Current.MainWindow.Width)
            Element.Width = Element.Width + e.HorizontalChange < 0 ? 0 : Element.Width + e.HorizontalChange;
    }

    protected override Visual GetVisualChild(int index)
    {
        return AdornerVisual[index];
    }
    protected override int VisualChildrenCount => AdornerVisual.Count;

    protected override Size ArrangeOverride(Size finalSize)
    {
        thumb.Arrange(new(AdornedElement.DesiredSize.Width,AdornedElement.DesiredSize.Height/2 ,5,AdornedElement.DesiredSize.Height/2));
        return base.ArrangeOverride(finalSize);
    }
}