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
namespace Avalonia3.Views
{
    public partial class MainWindow : Window
    {
        MainModelView modelView;
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
                modelView = (MainModelView)DataContext;
             
                TabLoaded(null, null);
            }
        }

        public void TabLoaded(object sender, EventArgs e)
        {
            if (modelView != null)
            {
                modelView.CreateTab();
            }
            else
            {
                // Poczekaj i spróbuj ponownie za chwilê.
                // ...
            }
        }
      
    }
}