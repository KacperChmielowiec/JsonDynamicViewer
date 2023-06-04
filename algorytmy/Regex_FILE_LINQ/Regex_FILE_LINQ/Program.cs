using Newtonsoft.Json.Converters;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Globalization;


//// obiekt patternn//

//Regex regex = new Regex(@"[A-Z0-9]+?", RegexOptions.IgnoreCase);

//// true false //

//string str = "Kacper Chmielowiec";

//if(regex.IsMatch(str)) Console.WriteLine(str);

//// return match or matches objects //

//MatchCollection matches = regex.Matches(str);
//MatchCollection matches2 = Regex.Matches(str, @"[A-Z0-9]+?");
//Match match = regex.Match(str);


//Dictionary<int, string> dic = new Dictionary<int, string>()
//{
//    { 1 , "string1" },
//};

//dic[2] = "string3";

//dic.TryAdd(3, "string3");

//dic.TryGetValue(3, out string test);

//Console.WriteLine(dic[1]);
////Console.WriteLine(DateTime.Now.ToString("d"));

////DateTime d1 = DateTime.Now.AddMonths(2);
////TimeSpan t = TimeSpan.FromTicks(DateTime.Now.Ticks);
////d1.Add(d1.TimeOfDay);

////DateTime d2 = new DateTime(t.Add(t).Ticks);

////DateTime d3 = d2.Add(TimeSpan.FromMilliseconds(d2.Millisecond));

////TimeSpan t2 = d2 - d1;

//DateTime dt = DateTime.Now;
//DateTime dt2 = new DateTime(1999, 01, 01, 12, 00, 00);

//TimeSpan t1 = dt - dt2;

//DateTime d3 = new DateTime(t1.Ticks);
//d3 = new DateTime(TimeSpan.FromSeconds(d3.Second).Ticks+60*60);
//d3 = d3.AddHours(1);
//StringBuilder sb  = new StringBuilder();


//sb.AppendFormat("NASZA DATA: {0}",d3.ToString());
//sb.Append("\n\r");
//sb.AppendFormat("NASZ CZAS: {0}", t1.Ticks);
//Console.WriteLine(sb.ToString());   

Action<List<string>> seriallize = (x) => Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(x.ToArray()));

Random r = new Random();

String[] s = new string[] {"A","B","C","D","E","F","G","H","J","K","L","Z","X","C"};

List<string> list = new List<string>();
for(int i = 0; i < s.Length; i++)
{
    list.Add(String.Join("",Enumerable.Range(0,10).Select(x => s[r.Next(s.Length)])));
}


List<string> list2 = new List<string>();
string s2 = "const";

list2 = list.Select(x => x + s2).ToList();

//list2 = (from l in list
        //let h = s2
       // where l != null && l.Length > 0
        //select l + h).ToList();

var c = (from str in CultureInfo.GetCultures(CultureTypes.AllCultures)
         select new { str, numFormat = str }
         into temp
         where temp.numFormat.NumberFormat.CurrencyDecimalDigits == 4
         select temp.str.Name
         ).ToList();
//list2 = list2.FindAll(x => x.Contains("B"));


var d2 = from str in list2
         group str by str.Contains("B") into n
         select new { key = n.Key, value = n};

 var d3 = list2.GroupBy(x => x.Length, x => x, (key, title) =>
    new { Author = key, Title = title });
foreach (var item in d3) { Console.WriteLine(item.Title); }

string pattern = @"(?<=_)(\d+)(?=\.txt)";

string path = "file_name_1_321.txt";

Console.WriteLine(Regex.Match(path, pattern).Value);













