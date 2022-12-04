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
        public static List<Team> teamList;
        private static Room room;
        private static Team winnerTeam;

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

            //Winnerにチェックが入っているほうを記録
            SetWinner(mw);

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

        public static void SetWinner(MainWindow mw)
        {
            if (mw.rdoWinnerA.IsChecked.Value)
            {
                winnerTeam = Team.findTeam(teamList, mw.cmbTeamA.SelectedValue.ToString());
            }
            else
            {
                winnerTeam = Team.findTeam(teamList, mw.cmbTeamB.SelectedValue.ToString());
            }
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
            gameHtmlText = Util.ReplaceHTMLTeamName(gameHtmlText, HardCording.TeamAlphaID, mw.cmbTeamA.SelectedValue.ToString());  //TeamAlpha Relpace
            gameHtmlText = Util.ReplaceHTMLTeamName(gameHtmlText, HardCording.TeamBravoID, mw.cmbTeamB.SelectedValue.ToString());  //TeamBravo Relpace
            File.WriteAllText(MainWindow.RunningPath + HardCording.GameHtmlPath_Suffix, gameHtmlText);

            //waiting.html
            var waitingHtmlText = File.ReadAllText(MainWindow.RunningPath + HardCording.WaitingHtmlPath_Suffix);
            waitingHtmlText = Util.ReplaceHTMLTeamName(waitingHtmlText, HardCording.TeamAlphaID, mw.cmbTeamA.SelectedValue.ToString());  //TeamAlpha Relpace
            waitingHtmlText = Util.ReplaceHTMLTeamName(waitingHtmlText, HardCording.TeamBravoID, mw.cmbTeamB.SelectedValue.ToString());  //TeamBravo Relpace
            waitingHtmlText = Util.ReplaceHTMLText(waitingHtmlText, HardCording.RoomID, mw.cmbRoomName.SelectedValue.ToString());    //Room Name
            for(int i=1; i<=5; i++)
            {
                waitingHtmlText = Util.ReplaceHTMLTeamName(waitingHtmlText, HardCording.GetMTID(i, true), room.GetTeamFromMt(i, true));  //Room Alpha
                waitingHtmlText = Util.ReplaceHTMLTeamName(waitingHtmlText, HardCording.GetMTID(i, false), room.GetTeamFromMt(i, false));//Room Bravo
            }
            var map = WowsMap.findMap(mapList, mw.cmbMaps.SelectedValue.ToString());
            waitingHtmlText = Util.ReplaceHTMLText(waitingHtmlText, HardCording.MapNameID, map.MapName);  //MapName
            waitingHtmlText = Util.ReplaceHTMLImageSource(waitingHtmlText, HardCording.MapImageID, HardCording.MapImagePathPrefix + map.ImageFileName);  //MapSource
            File.WriteAllText(MainWindow.RunningPath + HardCording.WaitingHtmlPath_Suffix, waitingHtmlText);

            //winner.html
            var winnerHtmlText = File.ReadAllText(MainWindow.RunningPath + HardCording.WinnerHtmlPath_Suffix);
            winnerHtmlText = Util.ReplaceHTMLTeamName(winnerHtmlText, HardCording.TeamAlphaID, mw.cmbTeamA.SelectedValue.ToString());  //TeamAlpha Relpace
            winnerHtmlText = Util.ReplaceHTMLTeamName(winnerHtmlText, HardCording.TeamBravoID, mw.cmbTeamB.SelectedValue.ToString());  //TeamBravo Relpace
            winnerHtmlText = Util.ReplaceHTMLText(winnerHtmlText, HardCording.RoomID, mw.cmbRoomName.SelectedValue.ToString());    //Room Name
            for (int i=1; i<=5; i++)
            {
                winnerHtmlText = Util.ReplaceHTMLTeamName(winnerHtmlText, HardCording.GetMTID(i, true), room.GetTeamFromMt(i, true));  //Room Alpha
                winnerHtmlText = Util.ReplaceHTMLTeamName(winnerHtmlText, HardCording.GetMTID(i, false), room.GetTeamFromMt(i, false));//Room Bravo
            }
            winnerHtmlText = Util.ReplaceHTMLTeamName(winnerHtmlText, HardCording.WinnerTeamID, winnerTeam.Name); //Winner TeamName
            winnerHtmlText = Util.ReplaceHTMLImageSource(winnerHtmlText, HardCording.WinnerLogoID, HardCording.TeamLogoPathPrefix + winnerTeam.ImageFileName);  //Team Logo
            File.WriteAllText(MainWindow.RunningPath + HardCording.WinnerHtmlPath_Suffix, winnerHtmlText);

        }
    }
}
