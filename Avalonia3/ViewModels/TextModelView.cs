using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.OpenGL.Surfaces;
using Avalonia3.Models;
using Avalonia3.References;
using Avalonia3.Services;
using Avalonia3.Views;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace Avalonia3.ViewModels
{
    public class TextModelView : DialogViewModelBase<ResultDialog>
    {
        public ICommand CancellCommand { get; set; }
        public ICommand AttemptCommand { get; set; }
        public TextWindow TextWindow { get; set; }
        public string JsonData { get; set; }
        public TextModelView()
        {
            CancellCommand = new DelegateCommand(Cancell);
            AttemptCommand = new DelegateCommand(Attempt);
            TextWindow = WindowReferences.TextWin;
            DialogResult = new ResultDialog() { Type = ResultType.Default };
        }
        private void Attempt(object window)
        {
            if (window is TextWindow TextWindow)
            {
                TextBox textBox = TextWindow.FindControl<TextBox>("JsonBox");
                try
                {
                    string json = textBox.Text;
                    JObject DataJson = JObject.Parse(json);
                    this.DialogResult = new ResultDialog() { Type = ResultType.Success};
                    JsonData = textBox.Text;
                    this.CloseDialog(window as TextWindow, DialogResult);

                }
                catch (Newtonsoft.Json.JsonException ex)
                {
                    textBox.Text = ex.Message;
                    textBox.Foreground = Brushes.Red;
                    textBox.FontSize = 15;
                }
                catch (NullReferenceException ex) 
                {
                    textBox.Text = ex.Message;
                    textBox.Foreground = Brushes.Red;
                    textBox.FontSize = 15;
                }
                catch (ArgumentNullException ex)
                {
                    textBox.Text = ex.Message;
                    textBox.Foreground = Brushes.Red;
                    textBox.FontSize = 15;
                }
                catch (Exception ex)
                {
                    Cancell(TextWindow);
                }
            }


        }
        private void Cancell(object window)
        {
            this.DialogResult = new ResultDialog() { Type = ResultType.Cancell };
            this.CloseDialog(window as TextWindow, DialogResult);
        }
       
    }
    
}
