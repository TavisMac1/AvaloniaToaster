using Avalonia.Media;

namespace AvaloniaToaster.Interfaces;

public interface IAvaloniaToasterThemes
{
    IBrush BackgroundColor { get; }
    IBrush ForegroundColor { get; }
}