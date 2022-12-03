using BroadcastTool.DataClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            //RoomのTextの設定
            if(room != null) mw.txtRoomDetails.Text = room.GetDetailText();

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

        public static void LoadRoom(MainWindow mw)
        {
            if (mw.cmbRoomName.SelectedValue == null) return;
            room = new Room(mw.cmbRoomName.SelectedValue.ToString());
        }
    }
}
