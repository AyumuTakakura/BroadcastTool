using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastTool.Language
{
    internal class LanguageBainder
    {
        /// <summary>
        /// 未実装
        /// マップの英名を受け取って、対応する言語のマップ名を返す
        /// </summary>
        /// <param name="mapName">英語のマップ名</param>
        /// <returns>対応する言語のマップ名</returns>
        public static string MapNameTranslate(string mapName)
        {
            return mapName;
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
    }
}
