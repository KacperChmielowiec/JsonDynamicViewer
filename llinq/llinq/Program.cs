// See https://aka.ms/new-console-template for more information


string[] words = "Witam na tym do świecie".Split(" ");


var query1 = words.GroupBy(w => w.Length, w => w.ToUpper()).Select(x => new { Length = x.Key, Words = x });

var query2 = from word in words
                group ( word.Substring(0,1).ToUpper()+word.Substring(1)) by word.Length into gr
                orderby gr.Key
                select new { Length = gr.Key, Words = gr };
                
foreach(var query in query2)
{
    Console.WriteLine(String.Format("LENGTH: {0}", query.Length));
    foreach(var q in query.Words)
    {
        Console.WriteLine(q);
    }
}