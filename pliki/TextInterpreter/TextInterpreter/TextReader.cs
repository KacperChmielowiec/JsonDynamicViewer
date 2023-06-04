using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace TextInterpreter
{
    internal static class TextReader
    {

       

        public static TextDataModel ReadJson(string path,string name)
        {
            string FullName = TextReader.pathCheck(path, name);
            if (FullName == null || FullName == "") throw new Exception("Nie udało sie znaleźć pliku o takiej nazwie/scieżce");
            try
            {
                // złamanie zasady solid o zależnosci ogółu od szczegółu 
                string json = File.ReadAllText(FullName);
                TextDataModel obj = JsonConvert.DeserializeObject<TextDataModel>(json);
                return obj;
            }
            catch (FileNotFoundException ex) { Console.WriteLine("Nie znaleźiono sieżki lub problem z wczytaniem :" + ex.Message); return null; }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
           
        }
        public static string ReadText(string path, string name)
        {

            string FullName = TextReader.pathCheck(path, name);
            if (FullName == null || FullName == "") throw new Exception("Nie udało sie znaleźć pliku o takiej nazwie/scieżce");
            try
            {
                string line = "";
                string AllText = "";
                using (StreamReader reader = new StreamReader(FullName))
                {
                    while((line = reader?.ReadLine()) != null)
                    {
                        AllText += line + "\n";
                    }
                }
                return AllText;
            }
            catch(FileNotFoundException ex) { Console.WriteLine("Nie znaleźiono sieżki lub problem z wczytaniem :" + ex.Message); return ""; }
            
        }
        private static string pathCheck(string path, string name)
        {
            
           string p =  path.Last() == '\\' ? path + name : path + @"\" + name;
           if (!Directory.Exists(path)) return "";
           return p;
        }

    }
}
