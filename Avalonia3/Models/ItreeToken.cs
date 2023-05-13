using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Models
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

    }
}
