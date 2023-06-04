using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;


namespace AvaloniaApplication2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Name { set; get; } = "Name";

        //public string Greeting => "Welcome to Avalonia!";

        //private int _counter = 0;

        //public int Counter
        //{
        //    get => _counter;
        //    set => this.RaiseAndSetIfChanged(ref _counter, value);
        //}

        //public ReactiveCommand<Unit, Unit> IncrementCommand { get; }

        //public MainWindowViewModel()
        //{
        //    IncrementCommand = ReactiveCommand.Create(IncrementCounter);
        //}

        //private void IncrementCounter()
        //{
        //    Counter++;
        //}

        public ObservableCollection<Node> Items { get; }
        public ObservableCollection<Node> SelectedItems { get; }
        public string strFolder { get; set; }

        public MainWindowViewModel()
        {
            strFolder = @"C:\Users\kacper\Desktop\Praca"; // EDIT THIS FOR AN EXISTING FOLDER

            Items = new ObservableCollection<Node>();

            Node rootNode = new Node(strFolder);
            rootNode.Subfolders = GetSubfolders(strFolder);

            Items.Add(rootNode);
        }

        public ObservableCollection<Node> GetSubfolders(string strPath)
        {
            ObservableCollection<Node> subfolders = new ObservableCollection<Node>();
            string[] subdirs = Directory.GetDirectories(strPath, "*", SearchOption.TopDirectoryOnly);

            foreach (string dir in subdirs)
            {
                Node thisnode = new Node(dir);

                if (Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly).Length > 0)
                {
                    thisnode.Subfolders = new ObservableCollection<Node>();

                    thisnode.Subfolders = GetSubfolders(dir);
                }

                subfolders.Add(thisnode);
            }

            return subfolders;
        }

        public class Node
        {
            public ObservableCollection<Node> Subfolders { get; set; }

            public string strNodeText { get; }
            public string strFullPath { get; }

            public Node(string _strFullPath)
            {
                strFullPath = _strFullPath;
                strNodeText = System.IO.Path.GetFileName(_strFullPath);
            }
        }



    }
}