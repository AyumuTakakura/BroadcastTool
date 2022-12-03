using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastTool.DataClass
{
    internal class Team
    {
        public string Name { get; }
        public string ImageFileName { get; }
        public string ImagePath { get; }

        public Team(string imagePath)
        {
            ImagePath = imagePath;
            ImageFileName = imagePath.Replace(Directory.GetParent(imagePath).FullName, "").Replace("\\", "");
            Name = ImageFileName
                .Split(".")
                .First();
        }

        /// <summary>
        /// TeamリストからComboBox用のString[]を生成する
        /// </summary>
        /// <param name="teamList">チームリスト</param>
        /// <returns>ComboBox用の文字列配列</returns>
        public static string[] toComboBoxStringArray(List<Team> teamList)
        {
            return teamList.Select(team => team.Name).ToArray();
        }

        public static Team findTeam(List<Team> teamList, string teamName)
        {
            return teamList.Find(team => team.Name == teamName);
        }
    }
}
