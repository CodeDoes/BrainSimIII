using Avalonia.Controls;
using Avalonia.Interactivity;

namespace BrainSimulatorAvalonia
{
    public partial class NotesDialog : Window
    {
        public NotesDialog()
        {
            InitializeComponent();
        }

        private void OKbutton_Click(object? sender, RoutedEventArgs e)
        {
            // TODO: Implement logic to save notes if needed
            this.Close();
        }

        private void Cancel_Click(object? sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
