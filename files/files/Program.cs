using System.IO;


string path = @"C:\Users\kacper\Desktop";
string file = "Plik.txt";
//tworzenie pliku

if (File.Exists(path + file)) { File.Delete(path + file); }

using (FileStream fs = File.Create(path + file))
{



}

using(StreamWriter fs2 = File.CreateText(path+file))
{

}

using(StreamReader fs3 = File.OpenText(path+file))
{

}

// apend/add to file
var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
char[] RandomWord = new char[8];
Random r = new Random();
string[] randomArr = new string[8]; 
for(int i = 0; i < 8; i++) { RandomWord = Enumerable.Range(0, 8).Select(x => chars[r.Next(chars.Length)]).ToArray(); randomArr[i] = RandomWord.ToString(); }
