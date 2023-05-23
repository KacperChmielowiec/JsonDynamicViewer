using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Interface
{
    public interface IDialogWindow 
    {
        bool? DialogResult { get; set; }
        object? DataContext { get; set; }

        public Task ShowDialog(Window owner);

        public void Close();
    }
}
