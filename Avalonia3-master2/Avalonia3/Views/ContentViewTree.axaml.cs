using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia3.Interface;
using Avalonia3.Models;
using Avalonia3.References;
using Avalonia3.ViewModels;
using System;
using System.Linq;

namespace Avalonia3.Views
{
    public partial class ContentViewTree : UserControl
    {
        MainModelView ModelView;
        public ContentViewTree()
        {
            InitializeComponent();
            ModelView = WindowReferences.MainModel;
        }
        private void Remove_LeftButtondDown(object sender, PointerPressedEventArgs args)
        {
            if (sender is Image img && img.Tag is TabItemContent)
            {
                ModelView.removeLeftButton();
            }
        }
        private void Load_Text_Event(object sender, PointerPressedEventArgs args)
        {
            if (sender is Control token)
            {
                ModelView.LoadText(token.Tag);
                ModelView.SetSelectedItem(token.Tag as ItreeToken);
            }
        }
        public void LoadSchame()
        {
            var control = this.FindControl<TabControl>("tabControl");
            if (control != null)
            {
                AvaloniaList<ITabItem> ListTab = new AvaloniaList<ITabItem>(TabControlReferences.Schemes.ToArray());
                control.Items = ListTab;

            }
        }
        public void SetTabItem(object sender, SelectionChangedEventArgs args)
        {
            if (ModelView != null)
                ModelView.SelectedTabItem = TabControlReferences.Schemes[TabControlReferences.Tab.SelectedIndex];
           
        }
    }
}
