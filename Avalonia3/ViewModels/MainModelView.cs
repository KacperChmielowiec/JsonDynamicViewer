using Avalonia.Controls;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MessageBox.Avalonia.BaseWindows.Base;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia3.Services;
using Avalonia3.Models;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using Avalonia.Collections;
using Avalonia3.Views;
using System.Collections.Generic;
using Avalonia.Controls.Generators;
using Avalonia;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using Avalonia3.References;
using Avalonia3.Interface;

namespace Avalonia3.ViewModels
{
    public partial class MainModelView : ObservableObject
    {
        public ICommand DialogCommand { get; set; }
        public ICommand LostCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public ICommand RemoveLeftButton { get; set; }
        public ICommand ModifyDialog { get; set; }

        public Avalonia3.Services.DialogFileServieces Dialog;

        public ItreeToken SelectedItem { get; set; }

        public Window Parent { get; set; }

        [ObservableProperty]
        private int _selected;

        [ObservableProperty]
        private bool _enable = false;

        [ObservableProperty]
        private string _text1 = "";

        [ObservableProperty]
        private bool _visibleIconEmpty = true;

        public ObservableCollection<ITabItem> Schemes { get; set; } = TabControlReferences.Schemes;

        private JsonServices _jsonServices;
        private TabControlService _tabControlService;
        public MainModelView()
        {
            DialogCommand = new RelayCommand(LoadDialog);
            LostCommand = new RelayCommand(LostFocus);
            RemoveLeftButton = new RelayCommand(removeLeftButton);
            _enable = false;
            Dialog = new DialogFileServieces("JsonDOM", ".json", "Text documents (.json)|*.json");
            MainModelView.instance = this;
            this.Selected = 0;
            Parent = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current.ApplicationLifetime).MainWindow;
            TreeContainerIndex s = new TreeContainerIndex();
            _jsonServices = new JsonServices();
            _tabControlService = new TabControlService(Parent);
          
        }
        public void removeLeftButton()
        {
            _tabControlService.RemoveItem();
        }
        public void CreateTab(ITabItem item)
        {
            _tabControlService.CreateItem(item);
        }
        public async void LoadText(object token)
        {
            try
            {
                _jsonServices.LoadTextJson(token);
                return;
            }
            catch (Exception ex)
            {
                await MessageService.ExceptionLoadDialog(ex.Message).Show();
            }
        }
        private async void LoadDialog()
        {
            var result = await MessageService.TabDialog().Show(Parent);

            if (result != null && result != ButtonResult.None)
            {
                try
                {
                    Guid guid = await Dialog.Show();

                    if (guid != Guid.Empty || guid != null)
                        _jsonServices.LoadDialogJson(result, guid);

                }
                catch (Exception ex)
                {
                    await MessageService.ExceptionLoadDialog(ex.Message).Show();
                }
            }
        }
        public void LostFocus()
        {
            Enable = false;
        }
       

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static MainModelView instance;

        public static MainModelView Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainModelView();
                }
                return instance;
            }

        }

    }
}
