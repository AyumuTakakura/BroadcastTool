using BroadcastTool.DataClass;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastTool.Language
{
    internal class LanguageBainder
    {
        public static bool isFirstInit { get; set; } = true;
        private static List<string> LanguageList { get; set; } = null;
        private static JdmCsvDictionary MapTrancelate { get; set; } = null;

        public static string selectedMapLanguage { get; set; } = string.Empty;

        public static void Initialize()
        {
            if (isFirstInit)
            {
                LanguageList = LoadLanguageFile();
                selectedMapLanguage = LanguageList.First();

            }

            LoadMapTrancelateFile();  //selectedMapLanguageがsetされてなければならない

        }

        /// <summary>
        /// 未実装
        /// マップの英名を受け取って、対応する言語のマップ名を返す
        /// </summary>
        /// <param name="mapName">英語のマップ名</param>
        /// <returns>対応する言語のマップ名</returns>
        public static string MapNameTranslate(string mapName)
        {
            var a = MapTrancelate.GetValue(mapName);
            return MapTrancelate.GetValue(mapName);
        }

        /// <summary>
        /// 未実装
        /// 対戦ルールの英名を受け取って、対応する言語のルールを返す
        /// </summary>
        /// <param name="mapRule">英語のルール名</param>
        /// <returns>対応する言語のルール名</returns>
        public static string MapRuleTranslate(string mapRule)
        {
            return mapRule;
        }

        public static string[] GetMapLanguageArray()
        {
            return LanguageList.ToArray();
        }

        public static void LoadMapTrancelateFile()
        {
            if (selectedMapLanguage == string.Empty)
            {
                MapTrancelate = new JdmCsvDictionary(HardCording.MapListFilePath, LanguageList.First());
            }
            else
            {
                MapTrancelate = new JdmCsvDictionary(HardCording.MapListFilePath, selectedMapLanguage);
            }
            
        }

        static List<string> LoadLanguageFile()
        {
            return File.ReadAllLines(HardCording.LanguageListFilePath)
                .ToList()
                .FindAll(s => s.Trim() != string.Empty);
        }
    }
}
