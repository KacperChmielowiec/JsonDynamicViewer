using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextInterpreter
{
    internal  class Counter
    {

        public  delegate void MyEventHandler();

        public  static event MyEventHandler MyEvent;

        public readonly int Interval = 100; 
        public int amount_char { get; set; } = 0;

        public  int begin_amount = 0;
        public Counter()
        {
        
            MyEvent += Show;
         
        }
        public  void Show()
        {
            int c = amount_char - begin_amount;
            c = c / this.Interval;
            Console.Clear();
            for(int i = 0; i < c; i++)
            {
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("POWIADOMIENIE O ZNAKACH !");
                Console.WriteLine($"DANE WPROWADZONE TO: {begin_amount+ (i * Interval) + Interval}");
                Console.WriteLine("---------------------------------------");



            }
            begin_amount = begin_amount + amount_char;
            Console.WriteLine("Całkowita Suma Znaków wprowadzonych przez Uzytkownika w sesji programu: " + begin_amount);
            Console.WriteLine("Całkowita Suma: " + Repository.DataList.Select(x => x.CharSum).Sum());
            Console.ReadKey();
        }
        public  void Call()
        {
           MyEvent?.Invoke();
        }
        public Counter Add(int sum)
        {
            this.amount_char = this.amount_char + sum;
            return this;
        }
        public void Substract()
        {
            throw new NotImplementedException("Nie zaimplementowałem tego  !!");
        }
        public void check()
        {
            if (begin_amount + Interval <= amount_char) this.Call();
        }

    }
}
