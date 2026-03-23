using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BrainSimulatorAvalonia.Modules
{
    public partial class ModuleTextFileDlg : ModuleBaseDlg
    {
        public ModuleTextFileDlg()
        {
            InitializeComponent();
        }

        private async void ImportButton_Click(object? sender, RoutedEventArgs e)
        {
            var statusText = this.FindControl<TextBlock>("StatusText");
            statusText.Text = "";
            var path = await BrowseAsync(true);
            if (string.IsNullOrWhiteSpace(path))
            {
                statusText.Text = "Choose a file first.";
                return;
            }
            if (!File.Exists(path))
            {
                statusText.Text = "File not found.";
                return;
            }
            try
            {
                this.FindControl<Button>("ImportButton").IsEnabled = false;
                this.FindControl<Button>("ExportButton").IsEnabled = false;
                // TODO: Call parent.theUKS.ImportTextFile(path) in Avalonia context
                await Task.Delay(100); // Placeholder for async import
                statusText.Text = "Success";
            }
            catch (Exception ex)
            {
                this.FindControl<Button>("ImportButton").IsEnabled = true;
                this.FindControl<Button>("ExportButton").IsEnabled = true;
                await ShowErrorAsync("Import failed.\n\n" + ex.Message);
            }
            finally
            {
                this.FindControl<Button>("ImportButton").IsEnabled = true;
                this.FindControl<Button>("ExportButton").IsEnabled = true;
            }
        }

        private async void ExportButton_Click(object? sender, RoutedEventArgs e)
        {
            var statusText = this.FindControl<TextBlock>("StatusText");
            var path = await BrowseAsync(false);
            if (string.IsNullOrWhiteSpace(path))
            {
                statusText.Text = "Choose a file first.";
                return;
            }
            try
            {
                this.FindControl<Button>("ImportButton").IsEnabled = false;
                this.FindControl<Button>("ExportButton").IsEnabled = false;
                // TODO: Call parent.theUKS.ExportTextFile(root, path) in Avalonia context
                await Task.Delay(100); // Placeholder for async export
                statusText.Text = "Success";
            }
            catch (Exception ex)
            {
                this.FindControl<Button>("ImportButton").IsEnabled = true;
                this.FindControl<Button>("ExportButton").IsEnabled = true;
                await ShowErrorAsync("Export failed.\n\n" + ex.Message);
            }
            finally
            {
                this.FindControl<Button>("ImportButton").IsEnabled = true;
                this.FindControl<Button>("ExportButton").IsEnabled = true;
            }
        }

        private async Task<string> BrowseAsync(bool open)
        {
            var dialog = open ? new OpenFileDialog() : new SaveFileDialog();
            dialog.Title = "Select UKS .txt file";
            if (open)
            {
                var result = await dialog.ShowAsync(this);
                return result != null && result.Length > 0 ? result[0] : string.Empty;
            }
            else
            {
                var result = await ((SaveFileDialog)dialog).ShowAsync(this);
                return result ?? string.Empty;
            }
        }

        private async Task ShowErrorAsync(string message)
        {
            var dlg = new Window
            {
                Title = "Error",
                Content = new TextBlock { Text = message },
                Width = 400,
                Height = 200
            };
            await dlg.ShowDialog(this);
        }
    }
}
