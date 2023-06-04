using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Pliki_eventy_Json // Note: actual namespace depends on the project name.
{
    

    internal class Program
    {
        static void Main(string[] args)
        {
            string json = @"{""value"":""kacper"", ""value1"": {""value3"":2}}";

            //object x = 12;

            //Console.WriteLine(new JsonElement().ValueKind.GetTypse());


            //JsonNode node = JsonValue.Create("string");

            //if(node is JsonValue)
            //{
            //    Console.WriteLine("is JsnValue !");
            //    if(node.AsValue().TryGetValue<string>(out string value))
            //    {
            //        Console.WriteLine(value);
            //    }
            //}

            //JsonObject obj = JsonNode.Parse(json).AsObject();

            //obj.TryGetPropertyValue("value", out JsonNode myNode);

            //JsonDocument Doc = JsonDocument.Parse(json);

            //JsonElement element = Doc.RootElement;

            //var property = element.GetProperty("value").EnumerateArray();

            //foreach(var el in property)
            //{
            //    Console.WriteLine(el.ToString());
            //}

            //byte[] buffor = Encoding.UTF8.GetBytes(json);


            //Utf8JsonReader reader = new Utf8JsonReader(buffor);

            //while(reader.Read())
            //{
            //    if(reader.TokenType == JsonTokenType.PropertyName && reader.ValueTextEquals("kacper"))
            //    {
            //        Console.WriteLine(reader.GetString());

            //    }
            //}



            //JObject jobject = JObject.Parse(json);

            //   //jobject.DescendantsAndSelf()
            //   //                         .OfType<JProperty>()
            //   //                         .Where(p => p.Name == "value").ToList().ForEach(p => p.Value = "Waldek");

            //JArray jarray = new JArray("kacper","chmielowiec");
            //Guid guid = Guid.NewGuid();
            //jarray.Add(new JObject(new JProperty("_id", guid.ToString())));

            //jobject.Add("array",jarray);

            //string newJson = JsonConvert.SerializeObject(jobject);

            //Console.WriteLine(newJson);


            JObject obj = new JObject();
            Guid guid = Guid.NewGuid();

            obj.Add("person", new JObject(
                new JProperty("name", "John"),
                new JProperty("age", 30)
                
            ));

         //wybieranie elementu za pomocą GUID-a
            
            obj.AddAnnotation(guid.ToString());

            Console.WriteLine(obj.Annotation<string>());
            string j = JsonConvert.SerializeObject(obj);
            Console.WriteLine(j);

        }
    }
}