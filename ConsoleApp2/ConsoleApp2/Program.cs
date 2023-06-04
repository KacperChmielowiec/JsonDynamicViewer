using System;
using System.Linq;


class TestClass
{



    public static bool compute(double[,] a)
    {
        int len = a.GetLength(0);
        for (int k = 0; k < len; k++)
        {
            double max = a[k, k];
            int r = k;
            for (int i = k; i < len; i++)
            {
                if (!(Math.Abs(a[i, k]) > Math.Abs(max))) continue;
                max = a[i, k];
                r = i;

            }
            if (max == 0) return false;
            for (int j = k; j < len + 1; j++)
            {
                double temp = a[k, j];
                a[k, j] = a[r, j];
                a[r, j] = temp;
            }
            for (int j = k; j < len + 1; j++)
            {
                a[k, j] = a[k, j] / max;
            }
            for (int i = 0; i < len; i++)
            {
                if (i == k) continue;
                else
                {
                    double p = a[i, k];
                    for (int j = k; j < len + 1; j++)
                    {
                        a[i, j] = a[i, j] - (p * a[k, j]);
                    }

                }
            }

        }
        return true;
    }









    static void Main(string[] args)
    {
        double[,] arr1 = { { 0, -1, 2, -2 }, { 2, 2, 1, 3 }, { 4, 1, 3, -1 } };
        double[,] arr2 = { { 3, 6, 1 }, { 2, 4, 2 } };
        double[,] arr3 = { { 0, 5, 15 }, { -2, 3, 7 } };

        double[][,] pack = { arr1, arr2, arr3 };

        foreach (var (a,index) in pack.Select((a,i) => (a,i+1)))
        {
            if(!compute(a))
            {
                
                Console.WriteLine($"Przykład nr {index}.\n\nMacierz układu jest osobliwa ! \n");
                continue;
            }
            int col = a.GetLength(1);
            double[] Values = Enumerable.Range(0, a.GetLength(0)).Select(x => (double) Math.Round(a[x,col-1],3)).ToArray();
            string equal = String.Format("Przykład nr {0}.\n\n",index) + "| " + String.Join(" | ", Values) + " |";
            equal += "\n";
            Console.WriteLine(equal);

        }
    }
}