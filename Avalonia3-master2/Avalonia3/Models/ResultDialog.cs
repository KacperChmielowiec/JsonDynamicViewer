using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Models
{
    public enum ResultType
    { 
        Cancell,
        Success,
        Default,

    }

    public class ResultDialog
    {
        public ResultType Type { get; set; } = ResultType.Default;
    }
}
