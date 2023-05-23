
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia3.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Models
{
    public class ObjectContext
    {
        public Guid Id { get; set; }
        public ItreeToken TreeToken { get; set; }

        public int deep { get; set; }
        public IEnumerable<ItreeToken> Children { get; set;}
        
        public ObjectContext(Guid _id, ItreeToken _obj, IEnumerable<ItreeToken> _chilldren) { 

                Id = _id;
                TreeToken = _obj;
                Children = _chilldren;
        
            
        }
      
    }
}
