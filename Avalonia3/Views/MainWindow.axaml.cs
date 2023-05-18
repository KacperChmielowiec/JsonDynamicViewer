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

namespace Avalonia3.Views
{
    public partial class MainWindow : Window
    {
        MainModelView ModelView;
        public MainWindow()
        {
            InitializeComponent();
            ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current.ApplicationLifetime).MainWindow = this;
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
                ModelView.CreateTab(new Models.TabItemContent() {Text = ""});
            }
            else
            {
                // Poczekaj i spróbuj ponownie za chwilê.
                // ...
            }
        }
        private void Remove_LeftButtondDown(object sender, PointerPressedEventArgs args)
        {
            if (sender is Image img)
            {
                TabControl tabControl = this.FindControl<TabControl>("tabControl") as TabControl;
                tabControl.Items = new AvaloniaList<TabItemContent>();
            }
        }

    }
}