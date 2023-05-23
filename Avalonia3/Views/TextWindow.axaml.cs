using Avalonia.Controls;
using Avalonia3.Interface;
namespace Avalonia3.Views
{
    public partial class TextWindow : Window, IDialogWindow
    {
        public bool? DialogResult {  get; set; }
        public TextWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }
}
