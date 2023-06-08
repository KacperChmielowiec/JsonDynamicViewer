using Avalonia3.ViewModels;
using Avalonia3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.References
{
    public static class WindowReferences
    {
        public static TextWindow TextWin {get;set;}
        public static MainModelView MainModel { get; set; } = new MainModelView();

        public static ContentViewTree ContentViewMain { get; set; }

        public static AdvanceView AdvanceViewMain { get; set; }
    }
}
