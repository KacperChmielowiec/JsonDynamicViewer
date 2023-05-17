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

namespace Avalonia3.ViewModels
{
    public partial class MainModelView : ObservableObject
    {


        public ICommand DialogCommand { get; set; }
        public ICommand LostCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public ICommand ModifyDialog { get; set; }

        public Avalonia3.Services.DialogFileServieces Dialog;

        public ItreeToken SelectedItem { get; set; }

        Window Parent { get; set; }

        [ObservableProperty]
        private int _selected;


        [ObservableProperty]
        private bool _enable = false;

        [ObservableProperty]
        private string _text1 = "";


        public ObservableCollection<TabItemContent> Schemes { get; set; } = new ObservableCollection<TabItemContent>();

        private IMsBoxWindow<ButtonResult> TabDialog()
        {
            return MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.YesNo,
                    ContentTitle = "Message",
                    ContentHeader = "Plik Json",
                    ContentMessage = "Czy chcesz otowrzyć nowe okno ",
                    WindowIcon = new WindowIcon("C:\\Users\\kacper\\source\\repos\\Avalonia3\\Avalonia3\\images\\question_mark.png"),
                    Icon = MessageBox.Avalonia.Enums.Icon.Question,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                });
        }


        public MainModelView()
        {


            DialogCommand = new RelayCommand(LoadDialog);
            LostCommand = new RelayCommand(LostFocus);
            RemoveCommand = new RelayCommand(RemoveItem);
            ModifyDialog = new RelayCommand(OpenModifyDialog);
            _enable = false;
            Dialog = new DialogFileServieces("JsonDOM", ".json", "Text documents (.json)|*.json");
            MainModelView.instance = this;
            this.Selected = 0;
            Parent = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current.ApplicationLifetime).MainWindow;
            TreeContainerIndex s = new TreeContainerIndex();
        }
        private void OpenModifyDialog()
        {

        }



        public void CreateTab(string header = "Nowa", string content = null, ItreeToken token = null)
        {

            Selected = Schemes.Count;
            header = String.Format("{0} {1}", header, Selected + 1);

            var _itemTab = new TabItemContent() { Header = header, Text = content, Tag = this.Schemes.Count };
            
            this.Schemes.Add(_itemTab);
            this.AddTab(Selected);


        }


        private void AddTab(int i)
        {

            //var tab = Application.Current.MainWindow.FindName("tabControl") as TabControl;
            var window = Parent as Window;
            var tab = window.FindControl<TabControl>("tabControl");

            var temp = tab.Items as AvaloniaList<TabItemContent> ?? new AvaloniaList<TabItemContent>();
            temp.Add(this.Schemes[i]);
            tab.Items = temp;
            tab.SelectedIndex = temp.Count - 1;


        




        }

        public void LoadText(object token)
        {
            try
            {
                this.LoadTextJson(token);
                return;
            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.Message + "\n" + "Problem z załadowaniem tekstu");
            }
        }

        private async void LoadDialog()
        {
            var result = await TabDialog().Show(Parent);

            if (result != null)
            {
                try
                {
                    Guid guid = await Dialog.Show();

                    if (guid != Guid.Empty)
                        this.LoadDialogJson(result, guid);

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message + "\n" + "Zła ścieżka albo plik o złej budowie !");
                }
            }



        }
        public void LostFocus()
        {
            Enable = false;
        }

        public void RemoveItem()
        {

            ItreeToken.JTokenType Type = SelectedItem.Type;
            this.RemoveItemJson(Type);

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
