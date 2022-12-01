using launchspace_desktop.pages;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace launchspace_desktop.components
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
            settingsPageButton.MouseUp += (sen, e) => { ((MainWindow)App.Current.MainWindow).SetPage(new SettingsPage()); };
            launcherPageButton.MouseUp += (sen, e) => { ((MainWindow)App.Current.MainWindow).SetPage(new LaunchersPage()); };
        }


        public void PageChanged(string pageName)
        {
            foreach(Control c in buttonsPanel.Children)
            {
                ListLabelButton  l = (ListLabelButton)c;
                l.ToggleOff();
            }

            if(pageName == LaunchersPage.PAGE_NAME)
            {
                launcherPageButton.ToggleOn();
            }
            else if(pageName == SettingsPage.PAGE_NAME)
            {
                settingsPageButton.ToggleOn();
            }
        }
    }
}
