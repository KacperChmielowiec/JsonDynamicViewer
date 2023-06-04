// See https://aka.ms/new-console-template for more information


using System.Security.Cryptography.X509Certificates;
using System.Text;

string path = @"/plik.txt";
FileStreamOptions options = new FileStreamOptions();
if(!File.Exists(path))
{
    using (StreamReader sr = new StreamReader(File.Create(path), System.Text.Encoding.UTF8, false, 512))
    {
        while (!sr.EndOfStream)
        {
            Console.WriteLine(sr.ReadLine());
        }

    }

    using( StreamReader sr = File.OpenText(path))
    {
        var x = 0;
        while((x =sr.Read()) != 0)
        {
            Console.WriteLine(x);
        }

    }
    using (StreamReader sr = File.OpenText(path))
    {

        char[] buff = new char[100];
        var count = 0;
        while (count < buff.Length)
        {
           var x = sr.Read(buff, 0+count,buff.Length);
           count += x;
           if (x == 0) break;
            
           
        }
        var temp = String.Empty;
        while((temp = sr.ReadLine()) != null)
        {
            Console.WriteLine(temp);
        }

    }

    File.ReadAllLines(path);
    byte[] b = File.ReadAllBytes(path);
    Console.WriteLine(Encoding.UTF8.GetString(b));



    using (FileStream fs = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read))
    {
        string str = "przykladowe slowo";
        byte[] b1 = Encoding.UTF8.GetBytes(str);
        var x = 0;
        fs.Write(b1, 0, b1.Length);

        



    }


}



