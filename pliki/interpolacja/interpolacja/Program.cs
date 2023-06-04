using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConsoleApp14
{
    class Program
    {
        public static double fill(int deep, double x)
        {

            if (deep == 0)
                return 1;
            else if (deep == 1)
            {
                return 2 * x;
            }
            else
            {
                return 2 * x * fill(deep - 1, x) - 2 * (deep - 1) * fill(deep - 2, x);
            }


        }

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

        public static double[,] addColumn(double[,] arr, double[] args)
        {

            int rowCount = arr.GetLength(0);
            int colCount = arr.GetLength(1);
            double[,] newArray = new double[rowCount, colCount+1];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount+1; j++)
                {
                    if (j < colCount)
                        newArray[i, j] = arr[i, j];
                    else
                        newArray[i, j] = args[i];

                }
            }
            return newArray;
        }



        public static Tuple<double,double>[] Core(double[,] matrix, double[] x_args, double[] y_args)
        {
            // słuzy do przechowywania sprawdzonego wyniku z interopolacyjnej funkcji + wynik źródłowy - Y )
            Tuple<double, double>[] check = new Tuple<double, double>[matrix.GetLength(0)];

            // zwykłe wypełnianie macierzy wg. rekurencyjnego wzoru dla funkcji bazowej 
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = fill(j, x_args[i]);
                }
            }
            Console.WriteLine("MACIERZ ZE WSPOLCZYNNIKAMI:\n");
            // zwykłe wypisywanie tablicy za pomocą dzielenia ich na wiersze 
            for (var i = 0; i < matrix.GetLength(1); i++)
            {
                
                double[] arr = Enumerable.Range(0, matrix.GetLength(0)).Select(x => matrix[i, x]).ToArray();
                Console.WriteLine(string.Join(" | ", arr));
            }
            Console.WriteLine("\n");

            // tablica z matrix_1 + kolumna z wyrazami wolnymi
            double[,] ArrayWithNodes = addColumn(matrix, y_args);

            // same współczynniki z tablicy wynikowej/jednostkowej - przypiszemy za chwile do niej wartości
            double[] coeff = new double[matrix.GetLength(0)];

            // obliczanie macierzy jednostkowej oraz wypisanie jak poprzednio
            if (compute(ArrayWithNodes))
            {
                Console.WriteLine("MACIERZ ZE WSPOLCZYNNIKAMI ORAZ WYRAZAMI WOLNYMI:\n");
                for (var i = 0; i < ArrayWithNodes.GetLength(0); i++)
                {
                    double[] arr = Enumerable.Range(0, ArrayWithNodes.GetLength(1)).Select(x => ArrayWithNodes[i, x]).ToArray();
                    coeff[i] = arr[arr.Count() - 1];
                    Console.WriteLine(string.Join(" | ", arr));
                }
            }
            else
            {
                Console.WriteLine("\n\nMacierz układu jest osobliwa ! \n"); return check;
            }

            // obliczenie Y dla każdego wiersza z macierzy i przypisaniu wyników do Tupli z początku razem z wartościami źródłowymi z tabelki dla Y
            for (var i = 0; i < matrix.GetLength(0); i++)
            {

                double sum = 0;
                double[] arr = Enumerable.Range(0, matrix.GetLength(1)).Select(x => matrix[i, x]).ToArray();
                // petla z przeksztalconym elementem na pare z indeksem
                foreach (var (element, index) in arr.Select((e, i) => (e, i)))
                {
                    sum += element * coeff[index];

                }
                check[i] = new Tuple<double, double>(Math.Round(sum,2), y_args[i]);

            }
            matrix = null;
            return check;

        }


        public static void start(double[] x_args, double[] y_args)
        {
            // tworze tablice do wypelniania z wynikami funkcji bazowych
            double[,] matrix_start = new double[x_args.Length, x_args.Length];

            
            Console.WriteLine("\n");
            // Y1 wynik / Y2 z tabelki (lewa strona pary i prawa) do porównania wyników obliczeń
            // Core funkcja wywołująca wszystkie potrzebne składniki - Core Engine - zwraca wynik końcowy
            Tuple<double, double>[] check = Core(matrix_start, x_args, y_args);

            Console.WriteLine("\n\n");
            Console.WriteLine("Y1 " + " Y2");
            foreach (var c in check)
            {
                
                Console.WriteLine(string.Join(" | ", c));
            }
            matrix_start = null;
            Console.WriteLine("\n");
        }










        static void Main(string[] args)
        {
            // służy do przechowywania funkcji bazowych ( ich wyników )
            
            double[] x_args = { 1.5, 2, 2.5, 3.5, 3.8, 4.1 };
            double[] y_args = { 2, 5, -1, 0.5, 3, 7 };

            double[] x_args2 = { -2, -0.5, 1.2, 3.0, 3.5, 5.0, 5.5 };
            double[] y_args2 = { 7, 5, 1, -0.5, 2, 1, -1 };


            //tablica do iteracji
            double[][][] main = new double[][][] { new double[][] { x_args, y_args }, new double[][] { x_args2, y_args2 } };

            // przekazuje w tablicy do funkcji rozruchowej
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine($"Przykład nr.{i+1}: ");
                start(main[i][0], main[i][1]);
            }
        }

    }
}
