
## Usage/Examples

In ``Project.cs`` add ``ToastNotificationService`` as a singleton.
```csharp
 services.AddSingleton<ToastNotificationService>();
```

In ``MainWindow.axaml.cs`` obtain a reference to the ``ToastNotificationService`` to register the main window.
```csharp
// MainWindow.axaml.cs constructor
 public MainWindow()
 {
     // Initializes MainWindow and its components.
     InitializeComponent();

     // Obtain a reference to the ToastNotificationService singleton
     var toastService = Program.Services.GetRequiredService<ToastNotificationService>();

     // Dispatch this main window to the ToastNotificationService instance
     toastService.RegisterMainWindow(this);
 }
```

View model controller example
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
