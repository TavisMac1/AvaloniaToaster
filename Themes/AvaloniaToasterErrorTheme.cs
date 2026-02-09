using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaToaster.Interfaces;

namespace AvaloniaToaster.Themes;

public class AvaloniaToasterErrorTheme : IAvaloniaToasterThemes
{
    public IBrush BackgroundColor => Avalonia.Media.Brushes.Red;

    public IBrush ForegroundColor => Avalonia.Media.Brushes.White;

    public HorizontalAlignment? HorizontalAlignment => null;

    public VerticalAlignment? VerticalAlignment => null;

    public double? BorderRadius => 3;
}
