using BroadcastTool.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastTool.Initializer
{
    internal class SettinsTab
    {
        public static void Initialize(MainWindow mw)
        {
            if (LanguageBainder.isFirstInit)
            {
                mw.cmbMapLanguage.ItemsSource = LanguageBainder.GetMapLanguageArray();
                mw.cmbMapLanguage.SelectedIndex= 0;
            }
        }
    }
}
