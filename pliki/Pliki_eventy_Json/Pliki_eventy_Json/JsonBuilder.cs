using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Data.Common;
using System.Security.Principal;

namespace Pliki_eventy_Json
{
    


    public class JsonBuilder
    {
        JsonObject Document;

        

        public JsonBuilder(JsonNodeOptions opt =  new JsonNodeOptions() )
        {
            bool isDefault = opt.Equals(new JsonNodeOptions());
            if (isDefault)
                Document = new JsonObject();
            else
                Document = new JsonObject(opt);

            

            
           

           
        }
      
        


        public bool isPropertyExist(JsonObject jsonObject, string propertyName)
        {
            // Sprawdzamy, czy właściwość istnieje w root obiekcie
            return false;
        }


    }
}
