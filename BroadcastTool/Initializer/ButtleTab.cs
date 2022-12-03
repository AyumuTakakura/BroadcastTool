using BroadcastTool.DataClass;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BroadcastTool.Initializer
{
    internal class ButtleTab
    {
        private static string mapFolderPath;
        private static string teamFolderPath;
        private static string roomCsvPath;
        private static List<WowsMap> mapList;
        private static List<Team> teamList;
        private static Room room;

        /// <summary>
        /// プログラムの実行ディレクトリを記憶し、
        /// Buttleで読み込むべきファイル群を読み込む
        /// </summary>
        public static void Initialize(MainWindow mw)
        {
            mapFolderPath = MainWindow.RunningPath + HardCording.MapFolderPath_Suffix;
            teamFolderPath = MainWindow.RunningPath + HardCording.TeamIconFolderPath_Suffix;
            roomCsvPath = MainWindow.RunningPath + HardCording.RoomCsvPath_Suffix;
            Reload(mw);
        }

        /// <summary>
        /// Buttleで読み込むべきファイル群を読み込む
        /// </summary>
        public static void Reload(MainWindow mw)
        {
            //Mapの読み込み
            mapList = Util.getFilePaths(mapFolderPath)
                .Where(path => Regex.IsMatch(path, @".?\.png|.*\.jpeg|.?\.jpg"))
                .Select(fileName => new WowsMap(fileName))
                .ToList();

            //MapをControleに設定
            mw.cmbMaps.ItemsSource = WowsMap.toComboBoxStringArray(mapList);
            mw.cmbMaps.SelectedIndex= 0;

            //MapのImageを読み込む
            LoadMap(mw);


            //Teamの読み込み
            teamList = Util.getFilePaths(teamFolderPath)
                .Where(path => Regex.IsMatch(path, @".?\.png|.*\.jpeg|.?\.jpg"))
                .Select(fileName => new Team(fileName))
                .ToList();

            //TeamAとBのComboBox設定
            mw.cmbTeamA.ItemsSource = Team.toComboBoxStringArray(teamList);
            mw.cmbTeamB.ItemsSource = Team.toComboBoxStringArray(teamList);
            mw.cmbTeamA.SelectedIndex = 0;
            mw.cmbTeamB.SelectedIndex = 0;

            //Team画像の設定
            LoadTeamA(mw);
            LoadTeamB(mw);


            //Roomのcomboboxの設定
            mw.cmbRoomName.ItemsSource = JdmCsvDictionary.GetLoadKeyArray(roomCsvPath);
            mw.cmbRoomName.SelectedIndex= 0;

            //Roomの設定
            LoadRoom(mw);

        }

        /// <summary>
        /// MainWindowのTeamA画像を読み込む
        /// </summary>
        /// <param name="mw">MainWindow</param>
        public static void LoadMap(MainWindow mw)
        {
            if (mw.cmbMaps.SelectedValue == null) return;
            var map = WowsMap.findMap(mapList, mw.cmbMaps.SelectedValue.ToString());
            mw.imgMapImage.Source = Util.getBitmapImage(map.ImagePath);
            mw.txtMapName.Text = map.MapName;
        }

        /// <summary>
        /// MainWindowのTeamA画像を読み込む
        /// </summary>
        /// <param name="mw">MainWindow</param>
        public static void LoadTeamA(MainWindow mw)
        {
            if (mw.cmbTeamA.SelectedValue == null) return;
            var team = Team.findTeam(teamList, mw.cmbTeamA.SelectedValue.ToString());
            mw.imgTeamA.Source = Util.getBitmapImage(team.ImagePath);
        }

        /// <summary>
        /// MainWindowのTeamB画像を読み込む
        /// </summary>
        /// <param name="mw">MainWindow</param>
        public static void LoadTeamB(MainWindow mw)
        {
            if (mw.cmbTeamB.SelectedValue == null) return;
            var team = Team.findTeam(teamList, mw.cmbTeamB.SelectedValue.ToString());
            mw.imgTeamB.Source = Util.getBitmapImage(team.ImagePath);
        }

        /// <summary>
        /// Roomをロードすると同時に、テキストの内容も書き換える
        /// </summary>
        /// <param name="mw"></param>
        public static void LoadRoom(MainWindow mw)
        {
            if (mw.cmbRoomName.SelectedValue == null) return;
            room = new Room(mw.cmbRoomName.SelectedValue.ToString());

            //RoomのTextの設定
            if (room != null) mw.txtRoomDetails.Text = room.GetDetailText();
        }

        /// <summary>
        /// HTMLへ適応する. 対象ファイルは以下
        /// game.html
        /// waiting.htl
        /// winner.html
        /// </summary>
        /// <param name="mw"></param>
        public static void ApplyToHTML(MainWindow mw)
        {
            //game.html
            var gameHtmlText = File.ReadAllText(MainWindow.RunningPath + HardCording.GameHtmlPath_Suffix);
            gameHtmlText = ReplaceHTMLText(gameHtmlText, HardCording.TeamAlphaID, mw.cmbTeamA.SelectedValue.ToString());  //TeamAlpha Relpace
            gameHtmlText = ReplaceHTMLText(gameHtmlText, HardCording.TeamBravoID, mw.cmbTeamB.SelectedValue.ToString());  //TeamBravo Relpace
            File.WriteAllText(MainWindow.RunningPath + HardCording.GameHtmlPath_Suffix, gameHtmlText);

            //waiting.html
            var waitingHtmlText = File.ReadAllText(MainWindow.RunningPath + HardCording.WaitingHtmlPath_Suffix);
            waitingHtmlText = ReplaceHTMLText(waitingHtmlText, HardCording.TeamAlphaID, mw.cmbTeamA.SelectedValue.ToString());  //TeamAlpha Relpace
            waitingHtmlText = ReplaceHTMLText(waitingHtmlText, HardCording.TeamBravoID, mw.cmbTeamB.SelectedValue.ToString());  //TeamBravo Relpace
            for(int i=1; i<=5; i++)
            {
                waitingHtmlText = ReplaceHTMLText(waitingHtmlText, HardCording.GetMTID(i, true), room.GetTeamFromMt(i, true));  //Room Alpha
                waitingHtmlText = ReplaceHTMLText(waitingHtmlText, HardCording.GetMTID(i, false), room.GetTeamFromMt(i, false));//Room Bravo
            }
            var map = WowsMap.findMap(mapList, mw.cmbMaps.SelectedValue.ToString());
            waitingHtmlText = ReplaceHTMLText(waitingHtmlText, HardCording.MapNameID, map.MapName);  //MapName
            waitingHtmlText = ReplaceHTMLImageSource(waitingHtmlText, HardCording.MapImageID, map.ImageFileName);  //MapSource
            File.WriteAllText(MainWindow.RunningPath + HardCording.WaitingHtmlPath_Suffix, waitingHtmlText);
            

            //winner.html

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
        static string ReplaceHTMLText(string htmlText, string id, string Text)
        {
            var before = "id='" + id + "'>.{2,7}</p>";
            var after = "id='" + id + "'>[" + Text + "]</p>";
            var result = Regex.Replace(htmlText, before, after);

            return result;
        }

        /// <summary>
        /// HTML上でのIDをもとに、画像のsrcを書き換える
        /// 例：ReplaceHTMLText("neko", "CAT");
        /// 置き換え前：＜p id='neko'＞NYANKO＜/p＞
        /// 置き換え後：＜p id='neko'＞NEKO＜/p＞
        /// 
        /// htmlへの要請として必ずsrcパラメータは最初に記述し、その次にidが来る用になっていること.
        /// htmlはシングルクォートでくくられていること.
        /// また、(htmlの書き方的に当たり前ではあるが)idはhtmlの中でユニーク(唯一、ほかで使用されていない)なものであること
        /// </summary>
        /// <param name="htmlText">HTMLのテキスト全文</param>
        /// <param name="id">HTMLのID</param>
        /// <param name="imageName">置き換えた後のText</param>
        /// <returns></returns>
        static string ReplaceHTMLImageSource(string htmlText, string id, string imageName)
        {
            var before = "<img src='.*' id='" + id + "'";
            var after = "<img src='" + imageName + "' id='" + id + "'";
            var result = Regex.Replace(htmlText, before, after);

            return result;
        }
    }
}
