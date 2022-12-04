using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BroadcastTool
{
    internal class Util
    {
        public static IEnumerable<string> getFilePaths(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return Directory.EnumerateFiles(directoryPath);
        }

        public static BitmapImage getBitmapImage(string path)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(path);
            image.EndInit();
            return image;
        }

        /// <summary>
        /// HTML上でのIDをもとに、そのパラメータ(Text)を変更する
        /// 例：ReplaceHTMLText("neko", "CAT");
        /// 置き換え前：＜p id='neko'＞[NYANKO]＜/p＞
        /// 置き換え後：＜p id='neko'＞[NEKO]＜/p＞
        /// 
        /// htmlへの要請として必ずidパラメータは最後に記述していること.
        /// htmlはシングルクォートでくくられていること.
        /// また、(htmlの書き方的に当たり前ではあるが)idはhtmlの中でユニーク(唯一、ほかで使用されていない)なものであること
        /// </summary>
        /// <param name="htmlText">HTMLのテキスト全文</param>
        /// <param name="idText">HTMLのID</param>
        /// <param name="Text">置き換えた後のText</param>
        public static string ReplaceHTMLTeamName(string htmlText, string id, string Text)
        {
            var before = "id='" + id + "'>.{2,7}</p>";
            var after = "id='" + id + "'>[" + Text + "]</p>";
            var result = Regex.Replace(htmlText, before, after);

            return result;
        }

        /// <summary>
        /// HTML上でのIDをもとに、そのパラメータ(Text)を変更する
        /// 例：ReplaceHTMLText("neko", "CAT");
        /// 置き換え前：＜p id='neko'＞NYANKO＜/p＞
        /// 置き換え後：＜p id='neko'＞NEKO＜/p＞
        /// 
        /// htmlへの要請として必ずidパラメータは最後に記述していること.
        /// htmlはシングルクォートでくくられていること.
        /// また、(htmlの書き方的に当たり前ではあるが)idはhtmlの中でユニーク(唯一、ほかで使用されていない)なものであること
        /// </summary>
        /// <param name="htmlText">HTMLのテキスト全文</param>
        /// <param name="idText">HTMLのID</param>
        /// <param name="Text">置き換えた後のText</param>
        public static string ReplaceHTMLText(string htmlText, string id, string Text)
        {
            var before = "id='" + id + "'>.*</p>";
            var after = "id='" + id + "'>" + Text + "</p>";
            var result = Regex.Replace(htmlText, before, after);

            return result;
        }

        /// <summary>
        /// HTML上でのIDをもとに、画像のsrcを書き換える
        /// 例：ReplaceHTMLText("dog", "inu.jpg");
        /// 置き換え前：＜img src='dog.jpg' id='dog'＞
        /// 置き換え後：＜img src='inu.jpg' id='dog'＞
        /// 
        /// htmlへの要請として必ずsrcパラメータは最初に記述し、その次にidが来るようになっていること.
        /// htmlはシングルクォートでくくられていること.
        /// また、(htmlの書き方的に当たり前ではあるが)idはhtmlの中でユニーク(唯一、ほかで使用されていない)なものであること
        /// </summary>
        /// <param name="htmlText">HTMLのテキスト全文</param>
        /// <param name="id">HTMLのID</param>
        /// <param name="imageName">置き換えた後のText</param>
        /// <returns></returns>
        public static string ReplaceHTMLImageSource(string htmlText, string id, string imageName)
        {
            var before = "<img src='.*' id='" + id + "'";
            var after = "<img src='" + imageName + "' id='" + id + "'";
            var result = Regex.Replace(htmlText, before, after);

            return result;
        }
    }
}
