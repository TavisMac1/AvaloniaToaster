# AvaloniaToaster

Create toast notifications in your Avalonia C# project without the hassle.

### OpenSSF
[![OpenSSF Best Practices](https://www.bestpractices.dev/projects/11466/badge)](https://www.bestpractices.dev/projects/11466)

## Usage/Examples

**Full example project**: https://github.com/TavisMac1/AvaloniaToasterExample

In ``Program.cs`` add ``ToastNotificationService`` as a singleton.
```csharp
 services.AddSingleton<ToastNotificationService>();
```

In ``MainWindow.axaml`` add a ``Panel`` into your main grid as follows:
```csharp
    <Panel x:Name="ToastContainer"
        Grid.ColumnSpan="3"
        ZIndex="1000"
        IsHitTestVisible="False"
    />
```

In ``MainWindow.axaml.cs`` obtain a reference to the ``ToastNotificationService`` to register the main window, and find the
``Panel`` you added in the previous.
```csharp
// MainWindow.axaml.cs constructor
 public MainWindow()
 {
     // Initializes MainWindow and its components.
     InitializeComponent();

     // Obtain a reference to the ToastNotificationService singleton
     var toastService = Program.Services.GetRequiredService<ToastNotificationService>();
     // Find the Panel you created in the previous step
     var toastContainer = this.FindControl<Panel>("ToastContainer");
     // Dispatch this main window to the ToastNotificationService instance
     toastService.RegisterMainWindow(toastContainer);
 }
```

### View model controller example
``` csharp
    public partial class ExampleClass : UserControl
    {
        private readonly ToastNotificationService _toastService;

        public ExampleClass(ToastNotificationService toastService) => _toastService = toastService;
        
        // The corresponding Avalonia xaml (axaml) file would have a button which triggers the following method to fire
        // The ToastNotificationService dispatches a toast with the specified text when this method is invoked
        public void ShowToastBtn_Click(object? sender, RoutedEventArgs e) => _toastService.Show("Toast Dispatched!");
    }
```

### Using Themes

Themes allow you to style the toast notification. You can create as many theme objects as you like.

Create a new class which implements the interface `IAvaloniaToasterThemes`. 
``` csharp
internal class ExampleTheme : IAvaloniaToasterThemes
{
    public IBrush BackgroundColor => Avalonia.Media.Brushes.Black;

    public IBrush ForegroundColor => Avalonia.Media.Brushes.White;

    HorizontalAlignment? IAvaloniaToasterThemes.HorizontalAlignment => null;

    VerticalAlignment? IAvaloniaToasterThemes.VerticalAlignment => null;

    double? IAvaloniaToasterThemes.BorderRadius => null;
} 
```

The method `Show` from `ToastNotificationService` accepts a theme as an optional argument. If you do not pass one in, it will use the default theme.
``` csharp
    public partial class ExampleClass : UserControl
    {
        private readonly ToastNotificationService _toastService;
        private ExampleTheme _exampleTheme; 
 
        public ExampleClass(ToastNotificationService toastService)
        {
            _toastService = toastService; // The ToastNotificationService instance from the DI container
            _exampleTheme = new(); // Your custom theme
        } 
        
        // The corresponding Avalonia xaml (axaml) file would have a button which triggers the following method to fire
        // The ToastNotificationService dispatches a toast with the specified text when this method is invoked
        public void ShowToastBtn_Click(object? sender, RoutedEventArgs e) => _toastService.Show("Toast Dispatched!", 3000, _exampleTheme);
    }
```

## Authors

[@TavisMac1](https://github.com/TavisMac1)

## License

[MIT](https://choosealicense.com/licenses/mit/)
