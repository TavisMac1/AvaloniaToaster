using Avalonia.Media;

namespace GoodnightComputer.Services;

public interface IAvaloniaToasterThemes
{
    IBrush BackgroundColor { get; }
    IBrush ForegroundColor { get; }
}