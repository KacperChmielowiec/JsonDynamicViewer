using Avalonia.Controls;
using Avalonia3.Interface;
using Avalonia3.Models;
using Avalonia3.ViewModels;
using Avalonia3.References;
using Avalonia.Input;
using Avalonia.Media;

namespace Avalonia3.Views
{
    public partial class TextWindow : Window, IDialogWindow
    {
        public bool? DialogResult {  get; set; }
        public TextWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            WindowReferences.TextWin = this;
        }
        private void ChangeFont(object? sender, GotFocusEventArgs arg)
        {
            var text = this.FindControl<TextBox>("JsonBox");
            text.Foreground = Brushes.Black;
        }
    }

}
