using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BroadcastTool.DataClass
{
    //外から見た感じはMap(JavaのLinkedHashMap)っぽく扱う
    internal class JdmCsvDictionary
    {
        public readonly string LoadKey;
        readonly string Path;
        List<string> Keys = new List<string>();
        List<string> Values = new List<string>();


        public JdmCsvDictionary(string path, string loadKey)
        {
            LoadKey= loadKey;
            Path = path;

            int i = 0;
            int loadIndex = 0;
            foreach(string line in File.ReadLines(path))
            {
                var values = line.Split(",")
                    .Select(s => s.TrimEnd())
                    .Select(s => s.TrimStart())
                    .ToList();

                if (++i == 1)
                {
                    loadIndex = values.IndexOf(loadKey);
                    continue;
                }

                if (values.Count <= loadIndex) continue;
                Keys.Add(values.First());
                Values.Add(values[loadIndex]);
            }
        }

        public string GetValue(string key)
        {
            return Values[Keys.IndexOf(key)];
        }

        public List<string> GetValueList()
        {
            return Values;
        }

        public List<string> GetKeyList()
        {
            return Keys;
        }

        public string[] GetLoadKeyArray()
        {
            return GetLoadKeyArray(Path);
        }

        public static string[] GetLoadKeyArray(string path)
        {
            var lst = File.ReadLines(path)
                .First()
                .Split(",")
                .Select(s => s.Trim())
                .ToList();
            lst.RemoveAt(0);

            return lst.ToArray();
        }
    }
}
