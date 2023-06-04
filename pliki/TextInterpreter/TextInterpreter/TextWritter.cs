using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace TextInterpreter
{
    internal static class TextWritter
    {
        
        public static bool WriteJson(string path, string name, TextDataModel obj)
        {
            if (Path.GetExtension(name) != ".json") throw new ArgumentException("Złe rozszerzenie pliku");
            string FullName = TextWritter.pathCheck(path, name);
            if (FullName == null || FullName == "") throw new Exception("Nie udało sie znaleźć pliku o takiej nazwie/scieżce");
            try
            {
               
                string json = JsonConvert.SerializeObject(obj,Formatting.Indented);
                File.WriteAllText(FullName, json);

                return true;
            }
            catch (IOException ex) { Console.WriteLine("Problem z zapisem pliku:" + ex.Message); return false; }
        }
        public static bool WriteText(string path,string name, string content)
        {
            
            if (Path.GetExtension(name) != ".txt") throw new ArgumentException("Złe rozszerzenie pliku");
            string FullName = TextWritter.pathCheck(path, name);
            if (FullName == null || FullName == "") throw new Exception("Nie udało sie znaleźć pliku o takiej nazwie/scieżce");
            try
            {
                using(StreamWriter sw = new StreamWriter(FullName))
                {
                    sw.Write(content);
                }


                return true;
            }
            catch (IOException ex) { Console.WriteLine("Problem z zapisem pliku:" + ex.Message); return false; }
            return true;
        }
        private static string pathCheck(string path, string name)
        {

            string p = path.Last() == '\\' ? path + name : path + '\\' + name;
            if (!Directory.Exists(path)) return "";
            return p;
        }

    }
}
