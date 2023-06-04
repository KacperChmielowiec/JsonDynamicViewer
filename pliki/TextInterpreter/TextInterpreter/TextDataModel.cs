using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextInterpreter
{
    public class Value
    {
        public char name;
        public int amount;
    
    
    }


    internal class TextDataModel
    {
        public TextDataModel(int ID, int Sum, List<Dictionary<int, Value>> Char)
        {

            this.IDText = ID;
            this.CharSum = Sum;
            this.CharCounts = Char;

        }
          


        public int IDText { get; set; }
        public int CharSum { get; set; }

        [JsonProperty("CharCounts")]
        public List<Dictionary<int, Value>> CharCounts { get; set; }

       

    }
}
