
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

namespace Avalonia3.Services
{
    public class JsonServices
    {
        private ObservableCollection<ITabItem> Schemes = TabControlReferences.Schemes;
        private MainModelView View = MainModelView.Instance;

        public void LoadTextJson(object token)
        {

            Guid _fileGuid = View.Schemes[View.Selected].File;

            var _file = JsonMap.files.Where(x => x.IdJson == _fileGuid).First();
            var _token = token as ItreeToken;
            var _item = _file.KeyValueMap[_token.Id];
            View.Schemes[View.Selected].Text = _item.TreeToken.ToString();
            View.SelectedItem = _item.TreeToken;
           
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
                 
                    View.Schemes[tab.SelectedIndex].Json = json as JContainerTree;
                    View.Schemes[tab.SelectedIndex].File = guid;
                    View.Schemes[tab.SelectedIndex].ctx = _data;

                    var temp = tab.Items as AvaloniaList<ITabItem>;
                    temp[tab.SelectedIndex] = View.Schemes[tab.SelectedIndex];
                    tab.Items = temp;

                    

                    View.Schemes[tab.SelectedIndex].Header = _data.path.Split("\\").Last();
                }
                else if (result == ButtonResult.Yes)
                {
                    View.VisibleIconEmpty = false;
                    View.CreateTab(new Models.TabItemContent() { Text = desc, Json = json as JContainerTree, File = guid, ctx = _data, Header = _data.path.Split("\\").Last(), Tag = View.Selected });
                }
               
                
            }
            else
            {
                return;
            }
           

         }
        public void RemoveItemJson(ItreeToken.JTokenType Type)
        {
            switch (Type)
            {
                
                case ItreeToken.JTokenType.Property:

                    JPropertyTree val_prop = (JPropertyTree)View.SelectedItem;
                    val_prop.Parent.Remove(val_prop);
                    break;

                case ItreeToken.JTokenType.Object:
                case ItreeToken.JTokenType.Array:

                    JContainerTree val_arr = (JContainerTree)View.SelectedItem;
                    if (val_arr.Parent != null)
                    {

                        val_arr.Parent.Remove(val_arr.Id);
                    }
                    else
                    {
                        View.Schemes[View.Selected].Json = null;
                    }
                    break;

            }
            View.Schemes[View.Selected].Text = String.Empty;

        }
      


    }
}
