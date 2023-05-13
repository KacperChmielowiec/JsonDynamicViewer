using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.ViewModel;
namespace Avalonia3.Services
{
    internal interface IDialogService
    {
        T OpenDialog<T>(DialogViewModelBase<T> vmodel);
       

    }
}
