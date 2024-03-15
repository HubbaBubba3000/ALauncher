
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace ALauncher.View;

public class ResizeAdorner : Adorner
{
    VisualCollection AdornerVisual;
    Thumb thumb;
    public ResizeAdorner(UIElement adornedElement, Thumb t) : base(adornedElement)
    {
        AdornerVisual = new VisualCollection(this);

        thumb = t;

        thumb.DragDelta += OnDragDelta;

        AdornerVisual.Add(thumb);

    }
    public double Width;
    public ResizeAdorner(UIElement adornedElement, Thumb t, DragCompletedEventHandler action) : base(adornedElement)
    {
        AdornerVisual = new VisualCollection(this);
        Width = ((FrameworkElement)adornedElement).Width;
        thumb = t;
        thumb.DragCompleted += action;
        thumb.DragDelta += OnDragDelta;

        AdornerVisual.Add(thumb);

    }
    private void OnDragDeltaWidth(object sender, DragDeltaEventArgs e)
    {
        if (Width + e.HorizontalChange < Application.Current.MainWindow.Width)
            Width = Width + e.HorizontalChange < 0 ? 0 : Width + e.HorizontalChange;
    }
    private void OnDragDelta(object sender, DragDeltaEventArgs e)
    {
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
        thumb.Arrange(new(AdornedElement.DesiredSize.Width, AdornedElement.DesiredSize.Height / 2, 5, AdornedElement.DesiredSize.Height / 2));
        return base.ArrangeOverride(finalSize);
    }
}