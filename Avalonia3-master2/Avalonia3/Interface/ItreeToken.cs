using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Avalonia3.Models;

namespace Avalonia3.Interface
{
    public interface ItreeToken
    {
        public enum JTokenType
        {
            Array, Object, Value, Property

        }
        Guid Id { get; set; }
        Guid ParentId { get; set; }

        JContainerTree Parent { get; set; }

        JTokenType Type { get; set; }

        public string ToString(int c);

        public ItreeToken GetValue();

        

    }
}
