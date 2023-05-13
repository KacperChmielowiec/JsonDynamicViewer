
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia3.ViewModels;
using Avalonia3.Models;
using MessageBox.Avalonia.Enums;

namespace Avalonia3.Services
{
    internal static class JsonServices
    {

        public static void LoadTextJson(this MainModelView View, object token)
        {

            Guid _fileGuid = View.Schemes[View.Selected].File;

            var _file = JsonMap.files.Where(x => x.IdJson == _fileGuid).First();
            var _token = token as ItreeToken;



            if (token is JObjectTree || token is JArrayTree)
            {


                var _item = _file.KeyValueMap[_token.Id];

                View.Schemes[View.Selected].Text = _item.TreeToken.ToString();
                View.SelectedItem = _item.TreeToken;
            }
            else
            {
                var _itemParent = _file.KeyValueMap[_token.ParentId];
                var _item = _itemParent.Children.First(x => x.Id == _token.Id);

                View.Schemes[View.Selected].Text = _item.ToString();
                View.SelectedItem = _item;

            }

            View.Enable = true;
            return;

        }
        public static void LoadDialogJson(this MainModelView View, ButtonResult result, Guid guid)
        {
           

            var _data = JsonMap.files.Where(x => x.IdJson == guid).FirstOrDefault();

            if (_data != null)
            {
                var _item = _data.KeyValueMap[_data.IdRoot];


                var json = _item.TreeToken;
                var desc = _item.TreeToken.ToString();

                var tab = Application.Current.MainWindow.FindName("tabControl") as TabControl;
                if (result == MessageBoxResult.No && View.Schemes.Count() > 0)
                {
                    View.Schemes[View.Selected].Text = desc;
          
                    View.Schemes[View.Selected].Json = json as JContainerTree;
                    View.Schemes[View.Selected].File = guid;
                    View.Schemes[View.Selected].ctx = _data;

                    tab.Items[View.Selected] = View.Schemes[View.Selected];
                    tab.SelectedIndex = View.Selected;

                    View.Schemes[View.Selected].Header = _data.path.Split("\\").Last();
                }
                else
                {
                    View.CreateTab();

                }
            }
            else
            {
                return;
            }
           

         }
        public static void RemoveItemJson(this MainModelView View,ItreeToken.JTokenType Type)
        {
            switch (Type)
            {
                
                case ItreeToken.JTokenType.Property:

                    JPropertyTree val_prop = (JPropertyTree)View.SelectedItem;
                    val_prop.Parent.Remove(val_prop);
                    break;
                case ItreeToken.JTokenType.Object:
                    JObjectTree val_obj = (JObjectTree)View.SelectedItem;
                    if (val_obj.Parent != null)
                    {
                        val_obj.Parent.Remove(val_obj);
                    }
                    else
                    {
                        View.Schemes[View.Selected].Json = null;
                    }
                    break;
                case ItreeToken.JTokenType.Array:
                    JArrayTree val_arr = (JArrayTree)View.SelectedItem;
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
