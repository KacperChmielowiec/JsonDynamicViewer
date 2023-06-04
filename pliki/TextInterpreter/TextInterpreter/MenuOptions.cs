using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TextInterpreter
{
    internal class MenuOptions
    {
        public Repository rep { get; set; }
        public Counter counter { get; set; }
        public MenuOptions(Repository rep, Counter c)
        {
            this.rep = rep;
            this.counter = c;
        }
        public void ShowMenu()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("Program Do Sprawdzania wystepowań znaków w tekście");
            str.AppendLine("\n");
            str.AppendLine("Wybierz opcje:\n");
            str.AppendLine("1. Sprawdź niegotowe teksty");
            str.AppendLine("2. Dodaj nowy tekst");
            str.AppendLine("3. Wykonaj sprawdzanie wybranego pliku");
            str.AppendLine("4. Usuń wybrany plik i powiązanie (.json)");
            str.AppendLine("5. Usuń wszystkie pliki i powiązania");
            str.AppendLine("6. Pokaż wszystkie pliki i powiązania (.json)");
            str.AppendLine("7. Pokaż ogólną Sume znaków ze sprawdzonych plików");
            str.AppendLine("8. Pokaż wszystkie znaki");
            str.AppendLine("9. Pokaż informacje o jednym pliku");
            Console.WriteLine(str.ToString());

        }
        public void GetNotReadyText()
        {
            var text = Repository.TextList.Where(x => x.Checked == false).Select(x => x.Name).ToList();
            
            Console.Clear();
            if (Repository.TextList.Count() == 0) { Console.WriteLine("Brak plików do sprawdzenia"); Console.ReadKey();  return; }
            if (text.Count > 0) {

                Console.WriteLine("--------------------");
                Console.WriteLine(String.Join(" | ", text));
                Console.WriteLine("--------------------");
            }
            else
            {
                Console.WriteLine("Wszystkie pliki są sprawdzone");
            }
            Console.ReadKey();

        }
        public void AddNewText(string path)
        {
            string pattern_name = @".*(?=\.txt)";
            path = path.Last() == '\\' ? path : path + "\\";
            Console.Clear();

            Console.WriteLine(new string('-',50));
            Console.WriteLine($"Umieść plik w: {path}");
            Console.WriteLine("podaj nazwe pliku poniżej: ");
            Console.WriteLine(new string('-', 50));
            string input = Console.ReadLine();

            if (File.Exists(path + input) && new FileInfo(path + input).Length > 0)
            {
                if (Path.GetExtension(path + input) == ".txt") {

                    int max = Repository.TextList.Select(x => x.IDtext).Max();
                    max++;
                    string name = Regex.Match(input, pattern_name).Value + $"_{max}.txt";
                    Directory.Move(path + input, Repository.PATH + name );
                    Repository.TextList.Add(new Text(max, Repository.PATH, name));


                    Console.Clear();
                    Console.WriteLine("Plik zosał dodany");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Plik ma złe rozszerzenie !");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Plik nie istnieje albo jest pusty!");
                Console.ReadKey();
            }
        }
        public void CheckFile()
        {
            string pattern = @"(?<=_)(\d+)(?=\.txt)";
            Console.Clear();
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"podaj nazwe pliku w scieżćę {Repository.PATH}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Pliki:\n");
            foreach (var file in Repository.TextList.Select(x => x).ToList())
            {
                Console.WriteLine(file.Name);
                if (!file.Checked) Console.Write("\n NIESPRAWDZONY");
            }
            Console.WriteLine("\n");
            
            string input = Console.ReadLine();

            if (File.Exists(Repository.PATH + input) && new FileInfo(Repository.PATH + input).Length > 0)
            {
                if (Path.GetExtension(Repository.PATH + input) == ".txt")
                {
                    int id = -999;
                    string id_str = Regex.Match(input,pattern).Value;
                    
                    bool canConvert = int.TryParse(id_str, out id);

                    if (id_str != "" && canConvert == true && id > 0)
                    {
                        if(Repository.TextList.Find(x => x.IDtext == id) != null)
                        {

                            if (Repository.DataList.Find(x => x.IDText == id) == null) {counter.Add(rep.CheckChoosenText(id)).check();}
                            rep.CheckChoosenText(id);
                            rep.CheckNotReady();
                           
                          
                         

                        }
                        else {Console.WriteLine("Nie ma takiego pliku w naszyc listach !"); Console.ReadKey(); Console.ReadKey(); }
                        Console.Clear();
                        Console.WriteLine("Plik zosałs sprawdzony");
                        Console.ReadKey();


                    }
                    



                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Plik ma złe rozszerzenie !");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Plik nie istnieje albo jest pusty");
                Console.ReadKey();

            }

        }
        public void DeleteOneFile()
        {
            Console.Clear();
            string pattern = @"(?<=_)(\d+)(?=\.txt)";
            Console.WriteLine($"Podaj nazwe pliku z: {Repository.PATH}");
            Console.WriteLine("Pliki:\n");
            foreach(var file in Repository.TextList.Select(x => x).ToList() )
            {
                Console.WriteLine(file.Name);
            }
            Console.WriteLine("\n");
            string input = Console.ReadLine();

            if (File.Exists(Repository.PATH + input) && new FileInfo(Repository.PATH + input).Length > 0)
            {
                if (Path.GetExtension(Repository.PATH + input) == ".txt" || Path.GetExtension(Repository.PATH + input) == ".json")
                {
                    int id = -999;
                    string id_str = Regex.Match(input, pattern).Value;

                    bool canConvert = int.TryParse(id_str, out id);

                    if (id_str != "" && canConvert == true && id > 0)
                    {
                        rep.Delete(id);
                        Console.Clear();
                        Console.WriteLine("Plik zosał Usuniety");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Plik ma złą nazwe 1");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Plik ma złe rozszerzenie !");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Plik nie istnieje albo jest pusty!");
                Console.ReadKey();
            }
        }
        public void DeleteAllFiles()
        {
            Console.Clear();
            if(rep.DeleteAllFiles())
            {
                Console.WriteLine("Udało sie usunąć pliki");
            }
            else
            {
                Console.WriteLine("Coś poszło nie tak");
            }
            Console.ReadKey();
        }

        public void ShowAllFiles()
        {
            Console.Clear();
            Console.WriteLine("Nazwa .txt  |  Nazwa .json");
            Console.WriteLine("---------------------------");
            
            foreach(var obj in Repository.TextList)
            {
                var dataJson = Repository.DataList.Where(x => x.IDText == obj.IDtext).FirstOrDefault();
                if (dataJson != null)
                {
                    Console.WriteLine($"{obj.Name} |  {Path.ChangeExtension(obj.Name, new string(".json"))}");
                }
                else
                {
                    Console.WriteLine($"{obj.Name} |  -  ");

                }
                Console.WriteLine("------------------------");
            }
            Console.ReadKey();

        }
        public void ShowSum()
        {
            Console.Clear();
            int sum = Repository.DataList.Count() > 0 ? Repository.DataList.Select(x => x.CharSum).Sum() : 0;
            int distinct = Repository.CharList.Count() > 0 ? Repository.CharList.Select(x => x.IDchar).Max() : 0;
            Console.WriteLine("---------");
            Console.WriteLine($"Suma: {sum} znaków.");
            Console.WriteLine($"Znaki bez powtórzeń: {distinct} znaków.");
            Console.WriteLine("---------");
            Console.ReadKey();
        }
        public void ShowAllData()
        {
           var NameFiles = Repository.DataList.Select(x => x.CharCounts).SelectMany(x => x)
                .Select(x => x).GroupBy(x => x.Keys.First())
                .Select(x => x).Distinct().ToList();

           
           Console.Clear();
           
           if (NameFiles.Count() == 0) Console.WriteLine("Brak danych...");
           for(int i = 0; i< NameFiles.Count(); i++)
           {
                var File = NameFiles[i].First();
                var c = Repository.CharList.Where(x => x.IDchar == File.Keys.First()).First();
                var files = Repository.TextList.Where(x => c.IDtext.Contains(x.IDtext)).Select(x => x.Name).ToList();
                var n = NameFiles[i].Select(x => x.Values.First()).Select(s => s.amount).Sum();
                

                Console.WriteLine($"IDchar: {File.Keys.First()} | Symbol: {File.Values.First().name} | Należy do: {string.Join(" |",files)}");
                Console.WriteLine("Ogólna ilość wystąpień: " + n );
                Console.WriteLine(new String('-', 70));
            }
            
            Console.Read();


            
                                                               
                                                  


        }
        public void ShowChooseData()
        {
            Console.Clear();
          
            Console.WriteLine($"Podaj nazwe pliku z: {Repository.PATH}");
            Console.WriteLine("Pliki:\n");
            foreach (var file in Repository.TextList.Select(x => x).ToList())
            {
                Console.WriteLine(file.Name);
            }
            Console.WriteLine("\n");
            string input = Console.ReadLine();


            if (File.Exists(Repository.PATH + input) && new FileInfo(Repository.PATH + input).Length > 0)
            {

                var id = Repository.TextList.Where(x => x.Name == input).Select(x => x.IDtext).First();
                (object d, List<Dictionary<int, Value>> c, int s) tup  = rep.GetInfo(id);
                if (tup.s > 0) { counter.Add(tup.s).check(); }

                var text = tup.d.GetType().GetProperty("text").GetValue(tup.d) as Text;
                var data = tup.d.GetType().GetProperty("data").GetValue(tup.d) as TextDataModel;
                Console.Clear();
                Console.WriteLine("Nazwa pliku: " +  text.Name);
                Console.WriteLine("IDtext: " + data.IDText + "\n");
                foreach (var info in data.CharCounts)
                {
                    Console.WriteLine("charID: " + info.Keys.First() + " | " + "charName: " + info.Values.First().name + " | " + "ilosc w pliku: " + info.Values.First().amount);

                }
                Console.WriteLine("\n");
                Console.WriteLine("Ogólna Suma znaków w pliku: " + data.CharSum);
              
                






            }
            else { Console.WriteLine("Plik nie istnieje albo jest pusty !"); }
            Console.ReadKey();

        }

    }
}
