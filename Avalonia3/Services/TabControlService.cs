using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia3.Interface;
using Avalonia3.Models;
using Avalonia3.References;
using Avalonia3.ViewModels;
using Avalonia3.Views;
using DynamicData;
using MessageBox.Avalonia.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Services
{
    public class TabControlService : ITabItemService
    {
        private Avalonia.Controls.Window Main;
        private ObservableCollection<ITabItem> Schemes;
        private MainModelView MainModelView;

        public event EventHandler<ITabItemArg> TabIconChange;
        public TabControlService(Avalonia.Controls.Window View) 
        { 
            Main = View;
            Schemes = TabControlReferences.Schemes;
            MainModelView = MainModelView.Instance;
            TabIconChange += SwapIcon;
        }
        public void RemoveItem()
        {
            var tab = Main.FindControl<TabControl>("tabControl");
            var selectedIndex = tab.SelectedIndex;
            var temp = tab.Items as AvaloniaList<ITabItem> ?? new AvaloniaList<ITabItem>();
            temp.RemoveAt(selectedIndex);
            this.Schemes.RemoveAt(selectedIndex);
            MainModelView.Selected -= 1;
            if(MainModelView.Selected == -1)
            {
                this.TabIconChange.Invoke(null,new TabItemArg());
            }
           
        }
        public void CreateItem(ITabItem item)
        {
            MainModelView.Selected = Schemes.Count;
            if (item.Header == null)
            {
                string header = String.Format("{0} {1}", "New", MainModelView.Selected + 1);
                item.Header = header;
            }
            if(item.Text == string.Empty) { item.IsVisible = false; }
            this.Schemes.Add(item);
            this.AddTab(MainModelView.Selected);
        }

        public void AddTab(int i)
        {
            var window = MainModelView.Parent as Window;
            var tab = window.FindControl<TabControl>("tabControl");

            var temp = tab.Items as AvaloniaList<ITabItem> ?? new AvaloniaList<ITabItem>();
            temp.Add(this.Schemes[i]);
            tab.Items = temp;
            tab.SelectedIndex = temp.Count - 1;
        }

        public void SwapIcon(object? sender, ITabItemArg arg)
        {
            if(MainModelView.VisibleIconEmpty == false)
                MainModelView.VisibleIconEmpty = true;
            else 
                MainModelView.VisibleIconEmpty = false;
        }

    }
}
