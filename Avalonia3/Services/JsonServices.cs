
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia3.ViewModels;
using Avalonia3.Models;
using MessageBox.Avalonia.Enums;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using Avalonia.Collections;
using System.Collections.ObjectModel;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Controls.Templates;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Avalonia3.Views;
using Avalonia3.References;
using Avalonia3.Interface;
using System.Threading.Tasks.Dataflow;

namespace Avalonia3.Services
{
    public class JsonServices
    {
        private ObservableCollection<ITabItem> Schemes = TabControlReferences.Schemes;
        private MainModelView View = MainModelView.Instance;

        public void LoadTextJson(ItreeToken token)
        {
            if (token != null)
                View.Schemes[View.Selected].Text = token.ToString();
            else
                View.Schemes[View.Selected].Text = string.Empty;
            View.Enable = true;
            
            return;
        }
        public void LoadDialogJson( ButtonResult result, Guid guid)
        {
           
            
            var _data = JsonMap.files.Where(x => x.IdJson == guid).FirstOrDefault();

            if (_data != null)
            {
                var _item = _data.KeyValueMap[_data.IdRoot];


                var json = _item.TreeToken;
                var desc = _item.TreeToken.ToString();

                var tab = View.Parent.Find<TabControl>("tabControl");


                if (result == ButtonResult.Cancel)
                { 
                    return;
                }
                else if (result == ButtonResult.No && View.Schemes.Count() > 0)
                {
                    View.VisibleIconEmpty = false;
                    View.Schemes[tab.SelectedIndex].Text = desc;
                 
                    View.Schemes[tab.SelectedIndex].Json = json as JObjectTree;
                    View.Schemes[tab.SelectedIndex].File = guid;
                    View.Schemes[tab.SelectedIndex].ctx = _data;
                    View.Schemes[tab.SelectedIndex].IsVisible = true;
                    var temp = tab.Items as AvaloniaList<ITabItem>;
                    temp[tab.SelectedIndex] = View.Schemes[tab.SelectedIndex];
                    tab.Items = temp;

                    

                    View.Schemes[tab.SelectedIndex].Header = _data.path.Split("\\").Last();
                }
                else if (result == ButtonResult.Yes)
                {
                    View.VisibleIconEmpty = false;
                    View.CreateTab(new Models.TabItemContent() { Text = desc, Json = json as JObjectTree, File = guid, ctx = _data, Header = _data.path.Split("\\").Last(), Tag = View.Selected, IsVisible = true});
                }
               
                
            }
            else
            {
                return;
            }
           

         }
        public void RemoveItemJson(ItreeToken item, int index)
        {
            if (item != null)
            {
                var value = item;
                if (value.Parent != null)
                {
                    value.Parent.Remove(value);
                    View.LoadText(this.Schemes[index].Json);
                }
                else
                {

                    this.Schemes[index].Json = null;
                    View.LoadText(this.Schemes[index].Json);
                    this.Schemes[index].IsVisible = false;

                }
            }
        }
      


    }
}
