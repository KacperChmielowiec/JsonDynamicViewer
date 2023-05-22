using Avalonia3.Interface;
using Avalonia3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.References
{
    public static class TabControlReferences
    {
        public static ObservableCollection<ITabItem> Schemes { get; set; } = new ObservableCollection<ITabItem>();

    }
}
