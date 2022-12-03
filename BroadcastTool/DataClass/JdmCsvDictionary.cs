using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastTool.DataClass
{
    internal class JdmCsvDictionary
    {
        readonly string LoadKey;
        readonly string Path;
        Dictionary<string, string> csvValue = new Dictionary<string, string>();

        public JdmCsvDictionary(string path, string loadKey)
        {
            LoadKey= loadKey;
            Path = path;

            int i = 1;
            int loadIndex = 0;
            foreach(string line in File.ReadLines(path))
            {
                var values = line.Split(",")
                    .Select(s => s.Trim())
                    .ToList();

                if (++i == 1)
                {
                    loadIndex = values.IndexOf(loadKey);
                    continue;
                }

                if (values.Count <= loadIndex) continue;
                csvValue.Add(values.First(), values[loadIndex]);
            }
        }

        public Dictionary<string, string> GetCsvtDictionary()
        {
            return csvValue;
        }

        public string[] GetLoadKeyArray()
        {
            var lst = File.ReadLines(Path)
                .First()
                .Split(",")
                .Select(s => s.Trim())
                .ToList();
            lst.RemoveAt(0);

            return lst.ToArray();
        }
    }
}
