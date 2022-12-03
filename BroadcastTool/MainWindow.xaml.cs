using BroadcastTool.Initializer;
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
        public static readonly string RunningPath = Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).FullName;
        MainViewModel vm = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = vm;

            ButtleTab.Initialize(this);
        }

        private void cmbMaps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtleTab.LoadMap(this);
        }

        private void cmbTeamA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtleTab.LoadTeamA(this);
        }

        private void cmbTeamB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtleTab.LoadTeamB(this);
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
    }
}
