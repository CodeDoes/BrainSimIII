using Avalonia.Controls;
using Avalonia.Interactivity;
using BrainSimulatorAvalonia.Modules;
using System;

namespace BrainSimulatorAvalonia.Modules
{
    public partial class ModuleStressTestDlg : ModuleBaseDlg
    {
        public ModuleStressTestDlg()
        {
            InitializeComponent();
        }

        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            if (this.FindControl<TextBox>("txtOutput") is TextBox txtOutput &&
                this.FindControl<TextBox>("textInput") is TextBox textInput)
            {
                txtOutput.Text = string.Empty;
                if (int.TryParse(textInput.Text, out int count))
                {
                    // TODO: Call ModuleStressTest.AddManyTestItems(count) and set output
                    txtOutput.Text = $"Added {count} test items.";
                }
                else
                {
                    txtOutput.Text = "Error! You must provide an integer to run.";
                }
            }
        }
    }
}
