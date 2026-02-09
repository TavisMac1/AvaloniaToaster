using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaToaster.Interfaces;

namespace AvaloniaToaster.Themes;

public class AvaloniaToasterSuccessTheme : IAvaloniaToasterThemes
{
    public IBrush BackgroundColor => Avalonia.Media.Brushes.Green;

    public IBrush ForegroundColor => Avalonia.Media.Brushes.White;

    public HorizontalAlignment? HorizontalAlignment => null;

    public VerticalAlignment? VerticalAlignment => null;

    public double? BorderRadius => null;
}
