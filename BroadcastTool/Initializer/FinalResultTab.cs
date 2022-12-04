using BroadcastTool.DataClass;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastTool.Initializer
{
    internal class FinalResultTab
    {
        public static void Initialize(MainWindow mw)
        {
            mw.cmb1stWinner.ItemsSource = Team.toComboBoxStringArray(ButtleTab.teamList);
            mw.cmb1stWinner.SelectedIndex= 0;
            mw.cmb2ndWinner.ItemsSource = Team.toComboBoxStringArray(ButtleTab.teamList);
            mw.cmb2ndWinner.SelectedIndex = 0;
            mw.cmb3rdWinner.ItemsSource = Team.toComboBoxStringArray(ButtleTab.teamList);
            mw.cmb3rdWinner.SelectedIndex = 0;
        }

        public static void Set1stTeamImage(MainWindow mw)
        {
            var team = Team.findTeam(ButtleTab.teamList, mw.cmb1stWinner.SelectedValue.ToString());
            mw.img1stTeam.Source = Util.getBitmapImage(team.ImagePath);
        }

        public static void Set2ndTeamImage(MainWindow mw)
        {
            var team = Team.findTeam(ButtleTab.teamList, mw.cmb2ndWinner.SelectedValue.ToString());
            mw.img2ndTeam.Source = Util.getBitmapImage(team.ImagePath);
        }

        public static void Set3rdTeamImage(MainWindow mw)
        {
            var team = Team.findTeam(ButtleTab.teamList, mw.cmb3rdWinner.SelectedValue.ToString());
            mw.img3rdTeam.Source = Util.getBitmapImage(team.ImagePath);
        }

        public static void ApplyToWinnerHTML(MainWindow mw)
        {
            Team first = Team.findTeam(ButtleTab.teamList, mw.cmb1stWinner.SelectedValue.ToString());
            Team second = Team.findTeam(ButtleTab.teamList, mw.cmb2ndWinner.SelectedValue.ToString());
            Team third = Team.findTeam(ButtleTab.teamList, mw.cmb3rdWinner.SelectedValue.ToString());

            //winner.html
            var finalResultHtmlText = File.ReadAllText(MainWindow.RunningPath + HardCording.FinalResultHtmlPath_Suffix);
            finalResultHtmlText = Util.ReplaceHTMLTeamName(finalResultHtmlText, HardCording.Winner1stNameID, mw.cmb1stWinner.SelectedValue.ToString());
            finalResultHtmlText = Util.ReplaceHTMLTeamName(finalResultHtmlText, HardCording.Winner2ndNameID, mw.cmb2ndWinner.SelectedValue.ToString());
            finalResultHtmlText = Util.ReplaceHTMLTeamName(finalResultHtmlText, HardCording.Winner3rdNameID, mw.cmb3rdWinner.SelectedValue.ToString());
            finalResultHtmlText = Util.ReplaceHTMLImageSource(finalResultHtmlText, HardCording.Winner1stImageID, HardCording.TeamLogoPathPrefix + first.ImageFileName);
            finalResultHtmlText = Util.ReplaceHTMLImageSource(finalResultHtmlText, HardCording.Winner2ndImageID, HardCording.TeamLogoPathPrefix + second.ImageFileName);
            finalResultHtmlText = Util.ReplaceHTMLImageSource(finalResultHtmlText, HardCording.Winner3rdImageID, HardCording.TeamLogoPathPrefix + third.ImageFileName);
            File.WriteAllText(MainWindow.RunningPath + HardCording.FinalResultHtmlPath_Suffix, finalResultHtmlText);
        }
    }
}
