
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Algorytm_Smitha_Watermana;
using Newtonsoft.Json;



public static class Program
{

    public static void smith_waterman(string seq1, string seq2)
    {
        int row = seq1.Length + 1;
        int col = seq2.Length + 1;

        int[,] matrix = new int[row,col];
        Array.Clear(matrix, 0, matrix.Length);

        int[,] tracing_matrix =  new int[row,col];
        Array.Clear(tracing_matrix, 0, tracing_matrix.Length);

        var max_index = (-1, -1);
        var max_score = -1;

        for(int i = 1; i < row; i++)
        {
            for(int j = 1; j < col; j++)
            {
                var match_value = seq1[i - 1] == seq2[j - 1] ? SCORE.MATCH : SCORE.MISMATCH;
                var diagonal_score = matrix[i - 1, j - 1] + match_value;
                
            }
        }


    }

    static void AddText(FileStream fs,string content)
    {
        content += "\n";
        byte[] info = new UTF8Encoding().GetBytes(content);

        fs.Write(info, 0, info.Length);

    }

    static string[] GetData()
    {
        return new string[] { "KACPER CHMIELOWIEC", "marcel chmielewski", "wrona", "korona" };
    }
    static void Main(string[] args)
    {
        string path = @"data\plik.txt";

        if(File.Exists(path) == false)
        {
           
            Directory.CreateDirectory(path.Split('\\')[0]);
            using (FileStream fs = File.Create(path))
            {
                string[] data = GetData();
                
                foreach(var (s,index) in data.Select((el,i) => (el,i+1)))
                {
                    AddText(fs, $"{index}. {s}");
                }

            }

        }
        
        int c = -1;
        string temp = string.Empty;
        using (FileStream fs = File.OpenRead(path))
        {
            byte[] b = new byte[fs.Length];
           
            while ((c = fs.Read(b, 0, b.Length)) > 0)
            {
                temp += Encoding.UTF8.GetString(b);
                Console.Write(temp);
               

            }

        }
        string[] seq = temp.Split('\n').SkipLast(1).ToArray();
        temp = JsonConvert.SerializeObject(seq);

        int[] matches = Array.ConvertAll(Regex.Matches(temp, @"[\d]+").Select(x => x.Value).ToArray(), x => int.Parse(x)).ToArray();

        if (matches.Max() % 2 != 0)
        {
            throw new Exception("Nie parzysta ilosc sekwencji !");
        }

        for (int i = 0; i < matches.Length / 2; i++)
        {
            string s1 = seq[i];
            string s2 = seq[i + 1];


        }

    }

}

