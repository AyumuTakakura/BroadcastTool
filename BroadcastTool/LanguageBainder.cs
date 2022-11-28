using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastTool
{
    internal class LanguageBainder
    {

        /// <summary>
        /// 将来的に複数の言語に対応するためのもの
        /// </summary>
        /// <param name="language"></param>
        /// <param name="main"></param>
        public static void textBinding(Language language, MainWindow main)
        {
            //Menu
            //main.DataContext = new { Menu_File = "File" };
        }

        /// <summary>
        /// EnumクラスのLanguage(int)を言語のstringへ変換します
        /// これはファイル参照時等に使います
        /// </summary>
        /// <param name="lng">Language</param>
        /// <returns>言語のstring</returns>
        private static string languageToString(Language lng)
        {
            switch (lng)
            {
                case Language.English:
                    return "en-us";
                case Language.Japanese:
                    return "ja-jp";
                default:
                    return "en-us";
            }
        }

    }

    public enum Language{
        Japanese,   //0
        English     //1
    }
}
