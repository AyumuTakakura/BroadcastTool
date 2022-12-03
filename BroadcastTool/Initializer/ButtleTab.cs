using BroadcastTool.DataClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        private static List<Room> roomList;

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

        public static void Reload(MainWindow mw)
        {

        }
    }
}
