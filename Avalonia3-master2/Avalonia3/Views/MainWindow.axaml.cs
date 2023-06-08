using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia3.ViewModels;
using System;
using System.Threading;
using System.Windows;
using System.Threading;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Shapes;
using Avalonia.Svg.Skia;
using Avalonia3.Models;
using Avalonia.Collections;
using Avalonia.Input;
using Avalonia;
using Avalonia.Diagnostics;
using Avalonia3.Interface;
using Avalonia3.References;

namespace Avalonia3.Views
{
    public partial class MainWindow : Window
    {
        MainModelView ModelView;
        public MainWindow()
        {
            InitializeComponent();
            ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current.ApplicationLifetime).MainWindow = this;
            ModelView = this.DataContext as MainModelView;
            DataContextChanged += OnDataContextChanged;
        }
        private async void OnDataContextChanged(object sender, EventArgs e)
        {
            if (DataContext != null)
            {
                ModelView = (MainModelView)DataContext;

                TabLoaded(null, null);
            }
        }
        public void TabLoaded(object sender, EventArgs e)
        {
            if (ModelView != null)
            {
                var curr = ModelView.CurrentContent.FindControl<TabControl>("tabControl");

                if (curr != null)
                {
                    var tempItem = new Models.TabItemContent() { Text = "", Tag = 0 };
                    
                    ModelView.CreateTab(tempItem);
                }
               
            }
            else
            {
                // Poczekaj i spróbuj ponownie za chwilê.
                // ...
            }
        }
     
    }
}