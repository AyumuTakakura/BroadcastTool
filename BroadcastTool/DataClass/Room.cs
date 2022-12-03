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
        JdmCsvDictionary Buttle { get; }

        public Room(string roomName)
        {
            Name = roomName;
            Buttle = new JdmCsvDictionary(MainWindow.RunningPath + HardCording.RoomCsvPath_Suffix, roomName);
        }

        public string getAlpha(string key)
        {
            return Buttle.GetCsvtDictionary()[key]
                .Split(HardCording.RoomSplitChar)
                .First();
        }

        public string getBravo(string key)
        {
            return Buttle.GetCsvtDictionary()[key]
                .Split(HardCording.RoomSplitChar)
                .Last();
        }

        public string[] GetRoomArray()
        {
            return Buttle.GetLoadKeyArray();
        }

    }
}
