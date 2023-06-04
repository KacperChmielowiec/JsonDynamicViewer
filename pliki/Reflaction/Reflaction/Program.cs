
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Serialization;
using static System.Console;
string example = String.Empty;

var data = example.GetType();

XmlSerializer serializer = new XmlSerializer(data);


