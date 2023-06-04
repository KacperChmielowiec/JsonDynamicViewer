using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextInterpreter
{
    internal class Char
    {
        public Char(int iDchar, int iDtext, char name)
        {
            IDchar = iDchar;
            IDtext = new List<int>(new int[] { iDtext });
            Name = name;
        }
    
        public int IDchar { get; set; }
        public List<int> IDtext { get; set; }
        public char Name { get; set; }
    }
}
