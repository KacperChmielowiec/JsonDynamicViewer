
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Models
{

    public class JsonFile
    {
        public JsonFile(Guid item, Dictionary<Guid, ObjectContext> keys,Guid root, string _path = null)
        {

            KeyValueMap = keys;
            IdJson = item;
            path = _path;
            IdRoot = root;


        }
        public Guid IdJson { get; set; }

        public Guid IdRoot { get; set; }
        public Dictionary<Guid, ObjectContext> KeyValueMap { get; set; } = new Dictionary<Guid, ObjectContext>();

        public string path { get; set; }

    }

    public static class JsonMap
    {
        public static List<JsonFile> files { get; set; } = new List<JsonFile>();
    }
}
