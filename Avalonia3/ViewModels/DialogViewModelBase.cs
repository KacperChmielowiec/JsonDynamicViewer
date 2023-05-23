using Avalonia3.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.ViewModels
{
    public class DialogViewModelBase<T>
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DialogViewModelBase(): this(string.Empty,string.Empty) { }
        public DialogViewModelBase(string title): this(title,string.Empty) { }
        public DialogViewModelBase(string title, string message)
        {
            Title = title;
            Message = message;
        }
        public T DialogResult { get; set; }

        public void CloseDialog(IDialogWindow dialog, T result)
        {
            DialogResult = result;
            dialog.Close();
        }
    }
}
