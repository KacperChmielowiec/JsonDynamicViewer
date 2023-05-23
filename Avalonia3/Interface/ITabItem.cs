using Avalonia3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Interface
{
    public interface ITabItem
    {
        public string Header { get; set; }

        public int Tag { get; set; }

        public Guid File { get; set; }

        public JsonFile ctx { get; set; }

        public JContainerTree Json { get; set; }

        public string Text { get; set; }

        public bool IsVisible { get; set; }
    }
}
