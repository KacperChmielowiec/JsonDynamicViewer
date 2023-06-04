using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace TextInterpreter 
{
    internal class Program
    {

        const string PATH = @"C:\Users\kacper\Desktop\programowanie\pliki\TextInterpreter\TextInterpreter\texts";
        const string PATH_NEW_TEXT = @"C:\Users\kacper\Desktop\programowanie\pliki\TextInterpreter\TextInterpreter\added";

        static void Main(string[] args)
        {

          
            Repository  repository = new Repository(PATH);
            

            if (Directory.GetFiles(PATH, @"*.txt").Length <= 2)
            {


                repository.DeleteAllFiles();
                Tuple<string, string>[] getSeed = new TextSeed().getStringData();

                foreach (Tuple<string, string> seed in getSeed)
                {
                    repository.AddText(seed.Item1, seed.Item2);
                }
                repository.CheckAllText();
                

            }
            else
            {
                repository.LoadData();
            }
            Counter counter = new Counter();
            MenuOptions menu = new MenuOptions(repository,counter);

            
            while (true)
            {
                menu.ShowMenu();
                int check = 0;
                string s = Console.ReadLine();
                var c = Int32.TryParse(s, out check);
                if (!c) check = 0;
                switch (check)
                {
                    case 1: { menu.GetNotReadyText(); Console.Clear(); break; }
                    case 2: { menu.AddNewText(PATH_NEW_TEXT); Console.Clear(); break; }
                    case 3: { menu.CheckFile(); Console.Clear(); break; }
                    case 4: { menu.DeleteOneFile(); Console.Clear(); break; }
                    case 5: { menu.DeleteAllFiles(); Console.Clear(); break; }
                    case 6: { menu.ShowAllFiles(); Console.Clear(); break; }
                    case 7: { menu.ShowSum(); Console.Clear(); break; }
                    case 8: { menu.ShowAllData(); Console.Clear(); break; }
                    case 9: { menu.ShowChooseData(); Console.Clear(); break; }
                    default: Console.Clear(); break;
                    
                }

            }










        }



    }
}