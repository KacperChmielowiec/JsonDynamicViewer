
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Models
{
    public enum JElement
    {
        ALLFILE,
        ALLPROP,
        CHOSENPROP,

    }
    internal class JsonContext
    {

        public JElement Element { get; set; }

        public Guid File { get; set; } = Guid.Empty;
        public Guid Item { get; set; } = Guid.Empty;

        public Guid[] Prop { get; set; }

        public JsonContext(Guid File, Guid Item, Guid[] prop)
        {
            Element = JElement.CHOSENPROP;

            this.File = File;
            this.Item = Item;
            this.Prop = prop;

        }
        public JsonContext(Guid File)
        {
            Element = JElement.ALLFILE;
            this.File = File;
        }
        public JsonContext(Guid File, Guid Item, JElement element = JElement.ALLPROP)
        {
            Element = element;
            this.File = File;
            this.Item = Item;

        }



    }
}
