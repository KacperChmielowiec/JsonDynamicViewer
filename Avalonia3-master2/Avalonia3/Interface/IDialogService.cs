using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia3.ViewModels;
namespace Avalonia3.Interface
{
    public interface IDialogService
    {    
        Task<T> OpenDialog<T>(DialogViewModelBase<T> model, Window Parent);
    }
}
