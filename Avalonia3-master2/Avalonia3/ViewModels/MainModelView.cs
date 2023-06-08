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
using AvaloniaEdit;
using AvaloniaEdit.Search;
using Avalonia3.Models;
using System.Reflection.Metadata;
using static SkiaSharp.HarfBuzz.SKShaper;
using Newtonsoft.Json.Linq;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia3.UserControls;
using Avalonia3.ViewModels.UserControls;

namespace Avalonia3.ViewModels
{

    public partial class MainModelView : ObservableObject
    {
        public ICommand DialogCommand { get; set; }
        public ICommand LostCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand OpenTextDialog { get; set; }
        public ICommand RemoveLeftButton { get; set; }
        public ICommand ModifyDialog { get; set; }
        public ICommand SwapContent { get; set; }
        public Avalonia3.Services.DialogFileServieces Dialog;

        public ItreeToken SelectedItem { get; set; }

        [ObservableProperty]
        private ITabItem _selectedTabItem;
        public Window Parent { get; set; }


        public TabControl TabControl { get; set; }

        [ObservableProperty]
        private int _selected;

        [ObservableProperty]
        private bool _enable = false;

        [ObservableProperty]
        private string _text1 = "";

        [ObservableProperty]
        private bool _visibleIconEmpty = false;

        [ObservableProperty]
        private UserControl _currentContent;

        [ObservableProperty]
        private ValueNodeViewModel _valueNode;

        public ObservableCollection<ITabItem> Schemes { get; set; } = TabControlReferences.Schemes;

        private JsonServices _jsonServices;
        private TabControlService _tabControlService;
        private AdvanceJsonService _advanceJsonService;
    


        public MainModelView()
        {
            MainModelView.instance = this;
            DialogCommand = new RelayCommand(LoadDialog);
            LostCommand = new RelayCommand(LostFocus);
            RemoveLeftButton = new RelayCommand(removeLeftButton);
            RemoveCommand = new DelegateCommand(RemoveItem);
            OpenTextDialog = new RelayCommand(OpenText);
            SwapContent = new RelayCommand(Swap);

            _enable = false;
            Dialog = new DialogFileServieces("JsonDOM", ".json", "Text documents (.json)|*.json");
            this.Selected = 0;
            TabControl = TabControlReferences.Tab;
            _valueNode = new ValueNodeViewModel();
            _jsonServices = new JsonServices();
            _tabControlService = new TabControlService(Parent);
            _advanceJsonService = new AdvanceJsonService();
           
        }
        public void Swap()
        {
            if (this.CurrentContent is ContentViewTree)
                this.CurrentContent = WindowReferences.AdvanceViewMain;
            else
                this.CurrentContent = WindowReferences.ContentViewMain;
        }
        public void RemoveItem(object item)
        {
            _jsonServices.RemoveItemJson(SelectedItem, TabControlReferences.Tab.SelectedIndex);
        }
        public void SetSelectedItem(ItreeToken item)
        {
            this.SelectedItem = item;
        }

        public void removeLeftButton()
        {
            _tabControlService.RemoveItem();
            if(TabControlReferences.Tab.SelectedIndex > -1)
            {
                this.SelectedTabItem = this.Schemes[TabControlReferences.Tab.SelectedIndex] ?? null;
            }
            else
            {
                this.SelectedTabItem = new TabItemContent() { Text = ""};
            }
        }
        public void CreateTab(ITabItem item)
        {
            this.SelectedTabItem = item;

            _tabControlService.CreateItem(item);
        }
        public async void LoadText(object token)
        {
            try
            {
                _jsonServices.LoadTextJson(token as ItreeToken);
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
                    string Content = await Dialog.ShowFileDialog(Parent);
                    if (!string.IsNullOrEmpty(Content))
                    {
                        Guid _guid = await _jsonServices.SetProcess(Content, Dialog.FileName,result);
                        if (_guid != Guid.Empty && _guid != null)
                        {
                            _advanceJsonService.LoadDataToChart(_guid,ValueNode);
                        }
                    }

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
        public async void OpenText()
        {
            try
            {
                var ServiceDialog = new TextDialogService();
                var Model = new TextModelView();
                await ServiceDialog.OpenDialog<ResultDialog>(Model, this.Parent);
                if (Model.DialogResult.Type == ResultType.Success)
                {
                    _jsonServices.SetProcess(Model.JsonData,"Z Pliku",ButtonResult.Yes);

                }

            }
            catch (Exception ex)
            {
                MessageService.ExceptionLoadDialog(ex.Message).Show();
            }
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
