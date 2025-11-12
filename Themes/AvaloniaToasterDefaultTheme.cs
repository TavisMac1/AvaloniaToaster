using Avalonia.Media;

namespace GoodnightComputer.Services;

internal class AvaloniaToasterDefaultTheme : IAvaloniaToasterThemes
{
    public IBrush BackgroundColor => Avalonia.Media.Brushes.Blue;

    public IBrush ForegroundColor => Avalonia.Media.Brushes.White;
}
