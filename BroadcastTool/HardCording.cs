using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastTool
{
    internal class HardCording
    {
        public const string RoomSplitChar = "|";

        public const string RoomCsvPath_Suffix = "\\csv\\Rooms.csv";
        public const string MapFolderPath_Suffix = "\\html\\maps";
        public const string TeamIconFolderPath_Suffix = "\\html\\team_icon";

        public const string GameHtmlPath_Suffix = "\\html\\game.html";
        public const string WaitingHtmlPath_Suffix = "\\html\\waiting.html";
        public const string WinnerHtmlPath_Suffix = "\\html\\winner.html";
        public const string FinalResultHtmlPath_Suffix = "\\html\\final_result.html";

        public const string TeamAlphaID = "TeamAlpha";
        public const string TeamBravoID = "TeamBravo";
        public const string MapNameID = "MapName";
        public const string MapImageID = "MapImage";
        public const string WinnerLogoID = "WinnerLogo";
        public const string WinnerTeamID = "WinnerTeam";
        public const string RoomID = "room";
        public const string Winner1stNameID = "1stName";
        public const string Winner2ndNameID = "2ndName";
        public const string Winner3rdNameID = "3rdName";
        public const string Winner1stImageID = "1stImage";
        public const string Winner2ndImageID = "2ndImage";
        public const string Winner3rdImageID = "3rdImage";

        public const string MapImagePathPrefix = "maps/";
        public const string TeamLogoPathPrefix = "team_icon/";

        public const string LanguageListFilePath = "csv\\LanguageList.txt";
        public const string MapListFilePath = "csv\\MapList.csv";

        public static string GetMTID(int i, bool isAlpha)
        {
            if (isAlpha)
            {
                return "mt" + i + "Alpha";
            }
            else
            {
                return "mt" + i + "Bravo";
            }
        }
    }
}
