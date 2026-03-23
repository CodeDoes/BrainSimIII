
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace BrainSimulatorAvalonia
{
    public partial class MainWindow : Window
    {
        // Ported fields from WPF
        public List<object> activeModules = new(); // TODO: Replace object with ModuleBase after port
        public List<string> pythonModules = new();
        public static string currentFileName = "";
        public static string pythonPath = "";
        // Begin port: ModuleHandler and UKS integration (stubs)
        // These will be replaced with actual implementations as porting continues
        // public static ModuleHandler moduleHandler = new();
        // public static UKS.UKS theUKS = moduleHandler.theUKS;
        // Example stub usage:
        // moduleHandler = new ModuleHandler();
        // theUKS = moduleHandler.theUKS;

        // TODO: Port ModuleHandler and UKS classes
        // public static ModuleHandler moduleHandler = new();
        // public static UKS.UKS theUKS = moduleHandler.theUKS;
        public static MainWindow theWindow = null;


        public MainWindow()
        {
            InitializeComponent();
            theWindow = this;
            SetTitleBar();
            this.Opened += MainWindow_Opened;
        }

        private void SetTitleBar()
        {
            this.Title = "Brain Simulator III " + System.IO.Path.GetFileNameWithoutExtension(currentFileName);
        }

        private async void MainWindow_Opened(object? sender, EventArgs e)
        {
            // TODO: Port pythonPath, moduleHandler, and UKS logic
            // Example: ShowMessage("MainWindow loaded (stub)");
        }

        // TODO: Port all methods and logic from WPF MainWindow.xaml.cs
        // Show a simple message box (Avalonia equivalent)
        async void ShowMessage(string message, string title = "Info")
        {
            var dialog = new Window
            {
                Title = title,
                Width = 400,
                Height = 150,
                Content = new StackPanel
                {
                    Children =
                    {
                        new TextBlock { Text = message, Margin = new Thickness(10) },
                        new Button { Content = "OK", HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center, Margin = new Thickness(10) }
                    }
                }
            };
            await dialog.ShowDialog(this);
        }

        async System.Threading.Tasks.Task<string?> ShowOpenFileDialogAsync()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Open File"
            };
            var result = await dialog.ShowAsync(this);
            return result != null && result.Length > 0 ? result[0] : null;
        }

        async System.Threading.Tasks.Task<string?> ShowSaveFileDialogAsync()
        {
            var dialog = new SaveFileDialog
            {
                Title = "Save File"
            };
            var result = await dialog.ShowAsync(this);
            return result;
        }

        // TODO: Port all other methods from WPF MainWindow.xaml.cs
    }
}
