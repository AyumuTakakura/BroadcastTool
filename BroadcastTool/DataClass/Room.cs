using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastTool.DataClass
{
    internal class Room
    {
        public string Name { get; }
        JdmCsvDictionary RoomCsv { get; }

        public Room(string roomName)
        {
            Name = roomName;
            RoomCsv = new JdmCsvDictionary(MainWindow.RunningPath + HardCording.RoomCsvPath_Suffix, roomName);
        }

        public string GetAlpha(string key) => RoomCsv.GetValue(key)
                .Split(HardCording.RoomSplitChar)
                .First();
        

        public string GetBravo(string key) => RoomCsv.GetValue(key)
                .Split(HardCording.RoomSplitChar)
                .Last();

        public string GetButtle(string key) => GetAlpha(key) + " vs " + GetBravo(key);
        

        public string GetDetailText()
        {
            var detail = Name;
            foreach (string k in RoomCsv.GetKeyList())
            {
                detail += "\r\n" + GetButtle(k);
            }
            return detail;
        }

        public string GetTeamFromMt(int mtNum, bool isAlpha)
        {
            var teams = RoomCsv.GetKeyList();

            if (isAlpha)
            {
                return GetAlpha(teams[mtNum - 1]);
            }
            else
            {
                return GetBravo(teams[mtNum-1]);
            }
        }

        public string[] GetRoomArray() => RoomCsv.GetLoadKeyArray();
        

    }
}
