using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Windows;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia;

namespace AvaloniaApplication2.Views
{
    public partial class TreeViewer : UserControl
    {
     
        private DispatcherTimer _timer;
        public string FirstName = "test";

        public TreeViewer()
        {
            InitializeComponent();
        }

        public void Load(string json)
        {

            JsonTreeView.Items = null;

            
            

            var children = new List<JToken>();

            try
            {
                var token = JToken.Parse(json);

                if (token != null)
                {
                    children.Add(token);
                }

                //JsonTreeView.Items = children;
              

            }
            catch (Exception ex)
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                  .GetMessageBoxStandardWindow("title", "Could not open the JSON string:\\r\\n\"" + ex.Message);
                                messageBoxStandardWindow.Show();
            }
        }

        //private async void JValue_OnMouseLeftButtonDown(object sender, 	PointerPressedEventArgs e)
        //{
        //    if (e.ClickCount != 2)
        //        return;

        //    var tb = sender as TextBlock;
        //    if (tb != null)
        //    {
        //        await Application.Current.Clipboard.SetTextAsync(tb.Text);
              
        //    }
           
        //}

        //private void ExpandAll(object sender, RoutedEventArgs e)
        //{
        //    ToggleItems(true);
        //}

        //private void CollapseAll(object sender, RoutedEventArgs e)
        //{
        //    ToggleItems(false);
        //}

        //private void ToggleItems(bool isExpanded)
        //{
          

        //    var prevCursor = Cursor;
        //    //System.Windows.Controls.DockPanel.Opacity = 0.2;
        //    //System.Windows.Controls.DockPanel.IsEnabled = false;
        //    Cursor = Cursors.Wait;
        //    _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.Normal, delegate
        //    {
        //        ToggleItems(JsonTreeView, JsonTreeView.Items, isExpanded);
        //        //System.Windows.Controls.DockPanel.Opacity = 1.0;
        //        //System.Windows.Controls.DockPanel.IsEnabled = true;
        //        _timer.Stop();
        //        Cursor = prevCursor;
        //    }, Application.Current.Dispatcher);
        //    _timer.Start();
        //}

        //private void ToggleItems(ItemsControl parentContainer, ItemCollection items, bool isExpanded)
        //{
        //    var itemGen = parentContainer.ItemContainerGenerator;
        //    if (itemGen.Status == Generated)
        //    {
        //        Recurse(items, isExpanded, itemGen);
        //    }
        //    else
        //    {
        //        itemGen.StatusChanged += delegate
        //        {
        //            Recurse(items, isExpanded, itemGen);
        //        };
        //    }
        //}

        //private void Recurse(ItemCollection items, bool isExpanded, ItemContainerGenerator itemGen)
        //{
        //    if (itemGen.Status != Generated)
        //        return;

        //    foreach (var item in items)
        //    {
        //        var tvi = itemGen.ContainerFromItem(item) as TreeViewItem;
        //        tvi.IsExpanded = isExpanded;
        //        ToggleItems(tvi, tvi.Items, isExpanded);
        //    }
        //}
    }
}
