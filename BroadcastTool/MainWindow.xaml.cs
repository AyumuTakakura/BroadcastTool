using BroadcastTool.Initializer;
using BroadcastTool.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BroadcastTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool Initializing = true;
        public static readonly string RunningPath = Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).FullName;
        MainViewModel vm = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = vm;

            Initialize(this);
        }

        public static void Initialize(MainWindow mw)
        {
            Initializing = true;

            LanguageBainder.Initialize();
            ButtleTab.Initialize(mw);
            FinalResultTab.Initialize(mw);
            SettinsTab.Initialize(mw);

            if(LanguageBainder.isFirstInit) LanguageBainder.isFirstInit= false;
            Initializing = false;
        }

        // ---------------- Bullte Tab -------------

        private void cmbMaps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtleTab.LoadMap(this);
        }

        private void cmbTeamA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtleTab.LoadTeamA(this);
            if(!Initializing) ButtleTab.SetWinner(this);
        }

        private void cmbTeamB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtleTab.LoadTeamB(this);
            if (!Initializing) ButtleTab.SetWinner(this);
        }

        private void cmbRoomName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtleTab.LoadRoom(this);

        }

        private void rdoWinnerA_Checked(object sender, RoutedEventArgs e)
        {
            vm.WinnerB = "False";
            BindingExpression be = rdoWinnerB.GetBindingExpression(RadioButton.IsCheckedProperty);
            be.UpdateTarget();
            ButtleTab.SetWinner(this);
        }

        private void rdoWinnerB_Checked(object sender, RoutedEventArgs e)
        {
            vm.WinnerA = "False";
            BindingExpression be = rdoWinnerA.GetBindingExpression(RadioButton.IsCheckedProperty);
            be.UpdateTarget();
            ButtleTab.SetWinner(this);
        }

        private void btnButtleApply_Click(object sender, RoutedEventArgs e)
        {
            ButtleTab.ApplyToHTML(this);
        }

        //  ----------- Final Result -------------

        private void cmb1stWinner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FinalResultTab.Set1stTeamImage(this);
        }

        private void cmb2ndWinner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FinalResultTab.Set2ndTeamImage(this);
        }

        private void cmb3rdWinner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FinalResultTab.Set3rdTeamImage(this);
        }

        private void btnWinnerApply_Click(object sender, RoutedEventArgs e)
        {
            FinalResultTab.ApplyToWinnerHTML(this);
        }

        // ----------- Settings --------------

        private void cmbMapLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Initializing)
            {
                LanguageBainder.selectedMapLanguage = cmbMapLanguage.SelectedValue.ToString();
                LanguageBainder.Initialize();
                Initialize(this);
                ButtleTab.LoadMap(this);
            }
        }


    }
}
