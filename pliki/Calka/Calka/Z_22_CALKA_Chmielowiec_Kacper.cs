using System;

namespace Calka 
{

   
    internal class Program
    {
        public const int a = 0;
        public const int b = 5;
        public const double eps = 0.0001;
        public static double Formula(double x)
        {
           return Math.Sin(Math.Sqrt(x));
        }


        public static double TrapMethod()
        {
            
            double Told = 0;
            double sum = 0;
            double m = 1;
            double h = b-a;
            int k = 0;
            double Tnew = (h / 2) * (Formula(a) + Formula(b));
            do
            {

               sum = 0;
                h = h / 2;
               m = 2 * m;
               k++;
               Told = Tnew;
               for (int i = 1; i < m; i += 1) sum += Formula(a + (i * h));
               Tnew = (h / 2) * (Formula(a) + Formula(b) + 2 * sum);


            } while (Math.Abs(Tnew - Told) >= eps);

            return Tnew;


        }


        public static double SimMethod()
        {
            double Told = 0;
            double sum1 = 0;
            double sum2 = 0;
            double m = 2;
            double h = (b - a)/m;
            int k = 1;
            double Tnew = (h / 3) * (Formula(a) + ( 4*Formula( (a+b)/2 ) ) + Formula(b) );
            do
            {

                sum1 = 0;
                sum2 = 0;
                h = h / 2;
                m = 2 * m;
                k++;
                Told = Tnew;
                for (int i = 1; i < m; i += 2)
                {
                    sum1 += Formula(a + (i * h));
                    if(i < m-1) sum2 += Formula(a + (i * h));
                }
                Tnew = (h / 3) * (Formula(a + (0 * h)) + Formula(a + (m * h)) + (4 * sum1) + (2*sum2));


            } while (Math.Abs(Tnew - Told) >= eps);

            return Tnew;
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Metoda Trapezów: "+ TrapMethod());
            Console.WriteLine("Metoda Parabol: " + SimMethod());
        }
    }
}