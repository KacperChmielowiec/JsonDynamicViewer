using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextInterpreter
{
    internal class Text
    {
        public Text(int iDtext, string path, string name)
        {
            IDtext = iDtext;
            TextPath = path.Last() != '\\' ? path+= @"\" : path;
            Name = name;
            
        }

        public int IDtext { get; set; }
        public string TextPath { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; } = false;

      
    }

}
    
