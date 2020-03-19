using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DemoSou
{
    class Program
    {
        static void Main(string[] args)
        {
            // 01
            string x = File.ReadAllText(@"D:\vn.txt");
            //string regex = "\\\"[a-zA-Z\\d_]+\\\"\\:\\{\"value\\\"\\:\\\"[a-zA-Z\\d\\sÄäÖöÜüẞß_]+\\\"\\}";
            //string replace = @"$1:{value:""***""}";
            //("[\w\d]+":{\"value\":)(\"[\w\d]*\")(})
            //https://www.mikesdotnetting.com/article/46/c-regular-expressions-cheat-sheet

            string pattern = @"(""[^""]*""):{""value"":""([^""]*)""}";
            string replace = @"$1:{value:""***""}";

            string input = @"{""SNR"":{""value"":""9999999""}}";

            var act = Regex.Replace(x, pattern, replace);


            // 02

            x = ProcessString(x, new [] { "TYP", "DOK", "BRI" });

            //var x1 = Regex.Replace(x, regex, replace);

            Console.WriteLine("Hello World!");
        }

        private static string ProcessString(string input, string[] sensitive)
        {
            string pattern = @"(""[^""]*""):{""value"":""([^""]*)""}";
            string replace = @"$1:{value:""***""}";

            MatchCollection matches = Regex.Matches(input, pattern);
            foreach (var item in matches)
            {
                
                var itemString = item.ToString();
                if (sensitive.Any(x => itemString.Contains(x)))
                {
                    input = input.Replace(itemString, Regex.Replace(itemString, pattern, replace));
                }               
            }

            var index = input.IndexOf("name=file");
            if (index >= 0)
            {
                input = input.Remove(index);
            }

            return input;
        }
    }
}
