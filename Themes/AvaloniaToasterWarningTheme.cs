using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaToaster.Interfaces;

namespace AvaloniaToasterDevProject.Themes;

public class AvaloniaToasterWarningTheme : IAvaloniaToasterThemes
{
    public IBrush BackgroundColor => Avalonia.Media.Brushes.Yellow;

    public IBrush ForegroundColor => Avalonia.Media.Brushes.Black;

    public HorizontalAlignment? HorizontalAlignment => null;

    public VerticalAlignment? VerticalAlignment => null;

    public double? BorderRadius => 3;
}
