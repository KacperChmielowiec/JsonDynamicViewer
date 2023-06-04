using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Interface
{
    public interface ITabItemService
    {
        public void RemoveItem();
        public void CreateItem(ITabItem item);

        public void AddTab(int i);

    }
}
