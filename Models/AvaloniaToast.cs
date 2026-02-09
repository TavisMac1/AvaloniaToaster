using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.Threading.Tasks;

namespace AvaloniaToaster.Models;

// Author: Tavis MacFarlane
// Copyright (c) 2025 Tavis MacFarlane
// License: MIT

internal class AvaloniaToast
{
    public Border Toast { get; init; }
    protected int Duration { get; init; }
    protected Action<AvaloniaToast> RemoveNatural { get; init; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved = false;

    public AvaloniaToast
    (
        Border t,
        int d,
        Action<Guid> removeAt,
        Action<AvaloniaToast> removeNatural
    )
    {
        Toast = t;
        Duration = d;
        t.PointerPressed += (s, e) => removeAt!(Id);
        RemoveNatural = removeNatural;

        FadeIn();
    }

    public void FadeIn()
    {
        Dispatcher.UIThread.Post(async () =>
        {
            for (double i = 0; i <= 1; i += 0.1)
            {
                if (IsRemoved) return;
                Toast!.Opacity = i;
                await Task.Delay(20);
            }
        });

        FadeOut();
    }

    public void FadeOut()
    {
        Task.Run(async () =>
        {
            await Task.Delay(Duration);

            for (double i = 1; i >= 0; i -= 0.1)
            {
                if (IsRemoved) return;
                Dispatcher.UIThread.Post(() => Toast!.Opacity = i);
                await Task.Delay(20);
            }

            RemoveNatural(this);
        });
    }
}
