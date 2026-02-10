using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using AvaloniaToaster.Interfaces;
using AvaloniaToaster.Services.Extensions;
using AvaloniaToaster.Themes;
using System;
using System.Collections.Generic;
using AvaloniaToaster.Models;
using System.Linq;

// Author: Tavis MacFarlane
// Copyright (c) 2025 Tavis MacFarlane
// License: MIT

namespace AvaloniaToaster;

public class ToastNotificationService
{
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    private Window? _mainWindow = null!;
    private Panel? _toastContainer = null!;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

    private IAvaloniaToasterThemes _defaultTheme = new AvaloniaToasterDefaultTheme();

    /// <summary>
    /// Use this if you have a simplistic layout (don't have multiple grids).
    /// </summary>
    /// <param name="window"></param>
    [Obsolete("This method is deprecated. Use RegisterMainWindow() for Panels instead.")]
    public void RegisterMainWindow(Window window) => _mainWindow = window;

    public void RegisterMainWindow(Panel toastContainerPanel) => _toastContainer = toastContainerPanel;

    private Queue<AvaloniaToast> _toasts = new();

    private Grid? _rootGrid { get; set; } = null!;

    private StackPanel? _toastPanel;

    /// <summary>
    /// Displays a toast notification overlay with the specified message on the registered main window.
    /// The notification will automatically disappear after the specified duration.
    /// </summary>
    /// <param name="message">The text to display in the toast notification.</param>
    /// <param name="durationMs">The duration in milliseconds the toast will be visible (default is 3000ms).</param>
    /// <param name="theme">The theme for the toast to implement. Must implement IAvaloniaToasterThemes</param>
    /// <exception cref="InvalidOperationException">Thrown if the main window has not been registered.</exception>
    public void Show
    (
        string message,
        int durationMs = 3000,
        IAvaloniaToasterThemes? theme = null
    )
    {
        if (_toastContainer == null)
            throw new InvalidOperationException("Main window not registered.");

        if (_toasts.Count >= 5)
        {
            var first = _toasts.Dequeue();
            RemoveAt(first);
        }

        if (theme is null) theme = _defaultTheme;

        var border = new Border
        {
            Background = theme.BackgroundColor,
            CornerRadius = theme.BorderRadius is not null ? new Avalonia.CornerRadius((double)theme.BorderRadius!) : new Avalonia.CornerRadius(2),
            Padding = new Avalonia.Thickness(16),
            Width = 320,
            Opacity = 0,
            BoxShadow = new Avalonia.Media.BoxShadows(new Avalonia.Media.BoxShadow
            {
                Blur = 8,
                Color = Avalonia.Media.Color.FromArgb((byte)(0.15 * 255), 0, 0, 0),
                OffsetX = 0,
                OffsetY = 2
            }),
            Child = new TextBlock
            {
                Text = message,
                Foreground = theme.ForegroundColor,
                FontSize = 15,
                TextAlignment = Avalonia.Media.TextAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                MaxWidth = 288
            },
            HorizontalAlignment = theme.HorizontalAlignment ?? Avalonia.Layout.HorizontalAlignment.Right,
            Margin = new Avalonia.Thickness(0, 0, 0, 10),
            ZIndex = 9999
        };

        var toast = new AvaloniaToast
        (
            t: border,
            d: durationMs,
            removeAt: RemoveAt,
            removeNatural: RemoveAt
        );

        _toasts.Enqueue(toast);

        if (_toastPanel == null)
        {
            _toastPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Avalonia.Thickness(0, 0, 40, 40),
                ZIndex = 9999
            };

            if (_toastContainer != null)
            {
                _toastContainer.Children.Add(_toastPanel);
            }
            else
            {
                _rootGrid = _mainWindow.FindRootGrid();
                if (_rootGrid != null)
                    _rootGrid.Children.Add(_toastPanel);
            }
        }

        _toastPanel.Children.Add(border);
    }

    private void RemoveAt(Guid id)
    {
        var toastToRemove = _toasts.Where((e) => e.Id.Equals(id)).FirstOrDefault();
        if (toastToRemove is null) return;
        var toast = toastToRemove.Toast;
        toastToRemove.IsRemoved = true;
        _toasts = new Queue<AvaloniaToast>(_toasts.Where(e => e.Id != id));
        Dispatcher.UIThread.Post(() => _toastPanel?.Children.Remove(toast));
        toastToRemove = null;
    }

    private void RemoveAt(AvaloniaToast toast)
    {
        Dispatcher.UIThread.Post(() => _toastPanel!.Children.Remove(toast.Toast));
        if (!toast.IsRemoved) _toasts.Dequeue();
    }
}