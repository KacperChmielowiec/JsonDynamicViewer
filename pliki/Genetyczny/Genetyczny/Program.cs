using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;

namespace Genetyczny // Note: actual namespace depends on the project name.
{
    internal static  class Program
    {
        static void fillArray(int[,] arr)
        {
            Random random = new Random();

            Enumerable.Range(0, arr.GetLength(0))
                .SelectMany(element => Enumerable.Range(0, arr.GetLength(1))
                , (el, i) => new { el, i }
                ).ToList().ForEach(x => arr[x.el, x.i] = random.Next(0, 2));

        }
        static int[] setFitness(int[,] parrents, int[] pattern)
        {
            var f = new int[parrents.GetLength(0)];
            for (int i = 0; i < parrents.GetLength(0); i++)
            {
                var temp = Enumerable.Range(0, parrents.GetLength(1)).Select(x => x = parrents[i,x]).ToArray();
                var count = temp.Zip(pattern, delegate (int x, int y) { return x == y ? (x == 1 ? 1 : 0) : 0; }).Count(x => x == 1);
                f[i] = count;
            }
            
            return f;
        }
        static int random(this int[] fitness, HashSet<int> set)
        {
            Random rnd = new Random();
            int size = fitness.Length;
            var range = Enumerable.Range(0, size).Where(x => !set.Contains(x)).ToArray();
            return range[rnd.Next(0,range.Count())];
        }
        static List<int> getIndexOfPrevailing(ref List<object> pairs)
        {
            List<int> list = new List<int>();
            pairs.ForEach(
                delegate(object x)
                {
                    var p1 = (int)x?.GetType().GetProperty("p1").GetValue(x);
                    var p2 = (int)x?.GetType().GetProperty("p1").GetValue(x);
                    var el = p1 >= p2 ? p1 : p2;

                    list.Add(el);

                }
            );

            return list;
        }



        static void Main(string[] args)
        {
            int[] pattern = new int[] { 1, 0, 0, 1, 1, 0, 0, 0, 0 ,1};
            int[,] parrent = new int[20, 10];
            int[] fitness = new int[20];

            fillArray(parrent);

            //Console.WriteLine(JsonConvert.SerializeObject(parrent));

            for(int i = 0; i < 100; i++)
            {
                Random rand = new Random();
                fitness = setFitness(parrent, pattern);
                
                HashSet<int> set = new HashSet<int>();
                List<object> pairs = new List<object>();
                while(set.Count() < fitness.Count())
                {
                    var value1 = fitness.random(set);
                    var value2 = fitness.random(set);
                    if (!set.Contains(value1) && !set.Contains(value2) && (value1 != value2))
                    {
                        set.Add(value1);
                        set.Add(value2);
                        pairs.Add(new { p1 = value1, p2 = value2 });
                       
                    }
                    
                }
                
                List<int> newGeneration = getIndexOfPrevailing(ref pairs);
                Console.WriteLine(JsonConvert.SerializeObject(newGeneration));



            }


        }



    }
}