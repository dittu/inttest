using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace CodatExam.Helper
{
    public static class TableExtensions
    {
        public static Dictionary<string,string> ToDictionary(this Table table)
        {
            var dictionary = new Dictionary<string, string>();
            foreach(var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            return dictionary;
        }
    }
}
