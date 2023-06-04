// See https://aka.ms/new-console-template for more information

using System.Text;

string path = Directory.GetCurrentDirectory();



void WriteToPath(FileStream fs, string content)
{

    byte[] info = new UTF8Encoding(true).GetBytes(content);
    fs.Write(info, 0, info.Length);

}

void ReadFromPath(FileStream fs, byte[] buffer, int offset, int length)
{
    if ((offset + length) > buffer.Length)
    {
        throw new ArgumentException("bufor jest za mały");
    }
    int count = 0;
    while(count < length)
    {
        int bytes = fs.Read(buffer, offset+count, length - count);
        if (bytes == 0) break;
        count += bytes;
        
    }
    string x = System.Text.Encoding.Default.GetString(buffer);
    Console.WriteLine(x);

}

using (FileStream stream = new FileStream("C:\\new.txt", FileMode.Create, FileAccess.ReadWrite))
{ 
    WriteToPath(stream, "test123");
}
using (FileStream stream = new FileStream("C:\\new.txt", FileMode.Open, FileAccess.ReadWrite))
{
    ReadFromPath(stream, new byte[100], 0, 100);
}

StreamWriter ws = new StreamWriter("C:\\new.txt",true);
ws.Write("kacper");
ws.Dispose();
StreamReader rs = new StreamReader("C:\\new.txt");
char[] buffer = new char[100];
rs.Read(buffer,0,10);
rs.Dispose();
string s = buffer.ToString();
Console.WriteLine(buffer);
int[,] x = new int[3, 3] { { 1,2,3},{ 1,2,3},{ 1,2,3} };

 void test(System.Array arr)
{

  int[] temp=  arr.Cast<int>().ToArray();
    

}
test(x);
