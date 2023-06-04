
using System.Runtime.ExceptionServices;
using System.Text.Json;
using System.Text.Json.Serialization;

string path = @"C:\Users\kacper\Desktop\programowanie\macierze\macierze\";
string[] file_content_1 = System.IO.File.ReadLines(path+"matrix_01.txt").ToArray();

int size = file_content_1.Count();
int[][] matrix_01 = new int[size][];
int it = 0;
foreach(string m in file_content_1)
{
    matrix_01[it] = m.Split(' ').ToArray().Select(x => int.Parse(x)).ToArray();
    it++;
}

string[] file_content_2 = System.IO.File.ReadLines(path+"matrix_02.txt").ToArray();

size = file_content_2.Count();
int[][] matrix_02 = new int[size][];
it = 0;
foreach (string m in file_content_2)
{
    matrix_02[it] = m.Split(' ').ToArray().Select(x => int.Parse(x)).ToArray();
    it++;
}

foreach(string m in file_content_1)
{
    Console.WriteLine(m);
}
Console.WriteLine('\n');
foreach (string m in file_content_2)
{
    Console.WriteLine(m);
}

int x = 0;
if (matrix_01.Count() == matrix_02[0].Count()) { size = matrix_01[0].Count(); x = 1; }
else if (matrix_01[0].Count() == matrix_02.Count()){ size = matrix_01.Count(); x = 2; }
int[][] matrix_03 = new int[size][];

switch(x)
{
    case 1:
        for (int j = 0; j < matrix_01[0].Count(); j++)
        {
            List<int> ints = new List<int>();
            for (int k = 0; k < matrix_02.Count(); k++)
            {
                int temp = 0;
                for (int i = 0; i < matrix_01.Count(); i++)
                {
                    
                    temp += matrix_01[i][j] * matrix_02[k][i];
                    
                }
                ints.Add(temp);

                //Console.WriteLine(string.Join("test - ", ints));

            }
            
            matrix_03[j] = ints.ToArray();

        }
        break;
    case 2:   

        for(int j = 0; j< matrix_01.Count(); j++)
        {
            List<int> ints = new List<int>();
            for (int k = 0; k < matrix_02[0].Count(); k++)
            {
                int temp = 0;
                for (int i = 0; i < matrix_01[0].Count(); i++)
                {

                    temp += matrix_01[j][i] * matrix_02[i][k];

                }
                ints.Add(temp);
            }
            matrix_03[j] = ints.ToArray();
        }
        break; 

    default: Console.WriteLine("BREAK"); break;
}

string resutl = JsonSerializer.Serialize(matrix_03);
Console.WriteLine("\n"+resutl);

foreach (var item in matrix_03.Select((value,i) => new {value = value, index = i } ))
{
    string write = string.Join(" ", item.value);
    Console.WriteLine(write);

    if (item.index == 0) System.IO.File.WriteAllText(path + "matrix_03.txt", write+"\n");
    else { System.IO.File.AppendAllText(path + "matrix_03.txt", write+"\n") ; }

}