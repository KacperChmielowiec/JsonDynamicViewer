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



        [ObservableProperty]
        private int _selected;


        [ObservableProperty]
        private bool _enable = false;




        public ObservableCollection<TabItemContent> Schemes { get; set; } = new ObservableCollection<TabItemContent>();

        private IMsBoxWindow<ButtonResult> TabDialog()
        {
            return MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.YesNo,
                    ContentTitle = "title",
                    ContentHeader = "header",
                    ContentMessage = "Message",
                    WindowIcon = new WindowIcon("icon-rider.png")
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

        }

        private void OpenModifyDialog()
        {

        }



        public void CreateTab(string header = "Nowa", string content = null, ItreeToken token = null)
        {

            Selected = Schemes.Count;
            header = String.Format("{0} {1}", header, Selected + 1);

            var _itemTab = new TabItemContent() { Header = header, Json = default(JContainerTree), Text = content, Tag = this.Schemes.Count };

            this.Schemes.Add(_itemTab);
            this.AddTab(Selected);


        }


        private void AddTab(int i)
        {

            //var tab = Application.Current.MainWindow.FindName("tabControl") as TabControl;
            var tab = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current.ApplicationLifetime).MainWindow.Find<Grid>("grid");


            //tab.Items.Add(this.Schemes[i]);
            //tab.SelectedIndex = tab.Items.Count - 1;




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
            var result = await TabDialog().Show();

            if (result != ButtonResult.Yes)
            {
                try
                {
                    Guid guid = Dialog.Show();

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
