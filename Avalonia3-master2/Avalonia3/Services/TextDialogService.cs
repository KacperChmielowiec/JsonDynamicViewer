using Avalonia3.Interface;
using Avalonia3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia3.ViewModels;
using Avalonia3.Views;
using Avalonia.Controls;

namespace Avalonia3.Services
{
    internal class TextDialogService : IDialogService
    { 
        public async Task<T> OpenDialog<T>(DialogViewModelBase<T> model, Window Parent)
        {
            IDialogWindow window = new TextWindow();
            window.DataContext = model;
            await window.ShowDialog(Parent);
            return model.DialogResult;
        }
    }
}
