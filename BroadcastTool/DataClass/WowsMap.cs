using BroadcastTool.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastTool.DataClass
{
    internal class WowsMap
    {
        public string MapName { get; }
        public string Rule { get; }
        public string NameWithRule { get; }
        public string ImagePath { get; }
        public string ImageFileName { get; }


        public WowsMap(string mapPath)
        {
            ImagePath = mapPath;
            ImageFileName = mapPath.Replace(Directory.GetParent(mapPath).FullName, "").Replace("\\", "");

            var srcNames = ImageFileName
                .Split(".")
                .First()
                .Split("_");

            MapName = LanguageBainder.MapNameTranslate(srcNames.First());
            Rule = LanguageBainder.MapRuleTranslate(srcNames.Last());
            NameWithRule = MapName + " " + Rule;
        }

        /// <summary>
        /// WowsMapのリストから、ComboBoxのItem用string配列を生成する
        /// </summary>
        /// <param name="mapList">WowsMapのリスト</param>
        /// <returns>ComboBoxのItem用string配列</returns>
        public static string[] toComboBoxStringArray(List<WowsMap> mapList)
        {
            return mapList.Select(map => map.NameWithRule).ToArray();
        }

        /// <summary>
        /// WowsMapのリストから、mapNameWithRule(ComboBoxのItem)を用いて該当のWowsMapを返す
        /// </summary>
        /// <param name="mapList">WowsMapのリスト</param>
        /// <param name="name">対応するImageのpath</param>
        /// <returns>名前に該当するWowsMap</returns>
        public static WowsMap findMap(List<WowsMap> mapList, string name)
        {
            return mapList.Find(map => map.NameWithRule == name);
        }
    }
}
