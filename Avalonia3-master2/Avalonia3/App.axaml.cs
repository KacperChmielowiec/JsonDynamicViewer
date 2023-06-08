using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia3.References;
using Avalonia3.ViewModels;
using Avalonia3.Views;
using System;

namespace Avalonia3
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            GC.KeepAlive(typeof(Avalonia.Styling.EventSetter).Assembly);
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                WindowReferences.MainModel = new MainModelView();
                WindowReferences.ContentViewMain = new ContentViewTree();
                WindowReferences.AdvanceViewMain = new AdvanceView();
                TabControlReferences.Tab = WindowReferences.ContentViewMain.FindControl<TabControl>("tabControl");
                WindowReferences.MainModel.CurrentContent = WindowReferences.ContentViewMain;
                desktop.MainWindow = new MainWindow()
                {

                    DataContext = WindowReferences.MainModel
                    
                };
                desktop.MainWindow.DataContext = WindowReferences.MainModel;
                WindowReferences.MainModel.Parent = desktop.MainWindow;

            }
           
            base.OnFrameworkInitializationCompleted();
        }
    }
}