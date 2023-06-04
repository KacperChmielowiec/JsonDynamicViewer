using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextInterpreter
{
    internal class Repository
    {
        public static List<Text> TextList = new List<Text>();
        public static List<Char> CharList = new List<Char>();
        public static List<TextDataModel> DataList = new List<TextDataModel>();
        

        public static string PATH;


        public Repository(string path)
        {
            PATH = path.Last() == '\\' ? path : path + '\\' ;
            

        }
        public void AddText(string name, string text,string path = "null")
        {
            if (path == "null") path = PATH;
            int count = Directory.GetFiles(path, @"*.txt").Length;
            count += 1;
            if (count > 2) return;
            text = Regex.Replace(text, @"^\s+", string.Empty, RegexOptions.Multiline);

            TextWritter.WriteText(path, name + "_" + count + ".txt", text);

            var TextObj = new Text(count, path, name + "_" + count + ".txt");

            TextList.Add(TextObj);


        }
        public  void CheckAllText()
        {
            string[] FileNames = Directory.GetFiles(PATH, @"*.txt");
            if (FileNames.Length <= 0)
            {
                Console.WriteLine("Brak plików do sprawdzenia");
                return;
            }

            CheckText(FileNames);
            this.CheckNotReady();





        }
        public int CheckChoosenText(int textID)
        {
            var data = TextList.Where(x => x.IDtext == textID).Select(x => new { path = x.TextPath, name = x.Name }).FirstOrDefault();
            int TotalSum = 0;
            if(data != null)
            {
                string path = data.path.Last() == '\\' ? data.path : data.path + "\\";
                string name = data.name;
                if(File.Exists(path+name))
                        TotalSum = CheckText(new string[] { path + name });
                else { Console.WriteLine("Nie ma takiego pliku !"); return 0; }
            }
            else { Console.WriteLine("Nie ma takiego pliku !"); return 0; }
            return TotalSum;
        }
        public int CheckText(string[] FileNames)
        {
            int id = 1;
            int TotalSum = 0;
            foreach (string fileName in FileNames)
            {
                int sum = 0;
                string TextOfFile = File.ReadAllText(fileName);

                string pattern = @"_(\d+)\.txt";
                var match = Regex.Match(fileName, pattern);
                int textID;
                if (match.Success)
                {
                    //Console.WriteLine(match.Groups[match.Groups.Count-1].Value);
                    textID = int.Parse(match.Groups[match.Groups.Count - 1].Value);
                }
                else
                {
                    return TotalSum;
                }

                Dictionary<char, int> charMap = new Dictionary<char, int>();

                foreach (char c in TextOfFile)
                {
                    if (char.IsWhiteSpace(c) || char.IsControl(c)) continue;
                    bool check = charMap.TryAdd(c, 1);
                    if (!check) charMap[c] = charMap[c] += 1;

                    if (CharList.Select(x => x.Name).Where(x => x == c).Count() == 0)
                    {
                        var charObj = new Char(id, textID, c);
                        CharList.Add(charObj);
                        id++;
                    }
                    else
                    {
                        foreach (var l in CharList)
                        {
                            if (l.Name == c) l.IDtext.Add(textID);
                        }
                    }



                   
                    sum++;
                    

                }
                List<Dictionary<int, Value>> separatedDicts = new List<Dictionary<int, Value>>();

                foreach (var item in charMap)
                {
                    Dictionary<int, Value> newDict = new Dictionary<int, Value>();

                    newDict.Add(CharList.Where(x => x.Name == item.Key).Select(x => x.IDchar).First(), new Value { name = item.Key, amount = item.Value });
                    separatedDicts.Add(newDict);
                }

                var DataObj = new TextDataModel(textID, sum, separatedDicts);
                DataList.Add(DataObj);
                TextWritter.WriteJson(PATH, Path.ChangeExtension(Path.GetFileName(fileName), ".json"), DataObj);

                TotalSum += sum;

            }

            return TotalSum;

        }
        public void LoadData()
        {
            string pattern_id = @"(?<=_)(\d+)(?=\.txt)";
            string pattern_name = @".*(?=\.txt)";
            string[] FileNames = Directory.GetFiles(PATH, @"*.txt");
            string[] JsonNames = Directory.GetFiles(PATH, @"*.json");
           
            foreach(var file in JsonNames)
            {
                var DataObj = TextReader.ReadJson(PATH, Path.GetFileName(file));
                DataList.Add(DataObj);
            }
            foreach(var file in FileNames)
            {

                int tempID = -999;
                int.TryParse(Regex.Match(file, pattern_id).Value,out tempID);
                var tempName = Path.GetFileName(file);

                if (tempName != "" && tempID > 0) {
                   
                    var TextObj = new Text(tempID, Path.GetDirectoryName(file), tempName);
                    TextList.Add(TextObj);
                }
            }
            var charList = DataList.GroupBy(x => x.IDText)
                                        .SelectMany(group => group.SelectMany(x => x.CharCounts)
                                            .Select(x => new { idText = group.Key, idChar = x.Keys.First(), name = x.Values.First().name }))
                                            .ToList();
                                                
            foreach(var c in charList)
            {

                if (CharList.Select(x => x.IDchar).Where(x => x == c.idChar).Count() > 0)
                {
                    var temp = CharList.Select(x => x).Where(x => x.IDchar == c.idChar).First();
                    CharList.Remove(temp);
                    temp.IDtext.Add(c.idText);
                    CharList.Add(temp);

                }
                else
                {
                    Char CharObj = new Char(c.idChar, c.idText, c.name);
                    CharList.Add(CharObj);
                }

            }
            this.CheckNotReady();







        }

        public void CheckNotReady()
        {


            List<int> txt_file_id = TextList.Select(x => x.IDtext).ToList();


            List<int> json_file_id = DataList.Select(x => x.IDText).ToList();
              
            List<int> leftOuterJoin = (from left in txt_file_id 
                                            select left).Except(json_file_id).ToList();

          

            TextList.Where(x => !leftOuterJoin.Contains(x.IDtext)).ToList().ForEach(x => x.Checked = true);
           






        }
        public bool DeleteAllFiles()
        {
            CharList.Clear();
            TextList.Clear();
            DataList.Clear();

            foreach (FileInfo file in new DirectoryInfo(PATH).GetFiles() ) file.Delete();
            return true;

        }
        public bool Delete(int idText)
        {
            string FileName = TextList.Where(x => x.IDtext == idText).Select(x => x.Name).First();
            CharList = CharList.Where(x => !x.IDtext.Contains(idText) || (x.IDtext.Contains(idText) && x.IDtext.Count() > 1)).ToList();
            CharList.Where(x => x.IDtext.Contains(idText)).ToList().ForEach(x => x.IDtext.Remove(idText));

            TextList = TextList.Where(x => x.IDtext != idText).Select(x => x).ToList();
            DataList = DataList.Where(x => x.IDText != idText).Select(x => x).ToList();

            
            foreach (FileInfo file in new DirectoryInfo(PATH).GetFiles())
            {
                if (file.Name == FileName) file.Delete();
                if (file.Name == Path.ChangeExtension(FileName,".json")) file.Delete();
            }
            return true;

        }
        public (object datai, List<Dictionary<int,Value>> chari, int sum) GetInfo(int id)
        {
            int sum = 0;

            if(Repository.DataList.Where(x => x.IDText == id).Count() == 0) sum+=CheckChoosenText(id);

            var fileinfo = Repository.DataList.Join(Repository.TextList, l => l.IDText, r => r.IDtext,(l,r) => new {data = l, text = r })
                                                                                                        .Where(x => x.data.IDText == id).First();
            var charinfo = fileinfo.data.CharCounts.ToList();




            return (fileinfo, charinfo, sum);
        }
      

    }
}
