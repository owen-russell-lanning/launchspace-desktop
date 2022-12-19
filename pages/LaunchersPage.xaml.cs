using launchspace_desktop.windows;
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

namespace launchspace_desktop.pages
{
    /// <summary>
    /// Interaction logic for LaunchersPage.xaml
    /// </summary>
    public partial class LaunchersPage : UserControl, ControlPage
    {

        public static readonly string PAGE_NAME = "launchers_page";
        public LaunchersPage()
        {
            InitializeComponent();
            newLauncherButton.SetText("New Launcher");
            newLauncherButton.SetSource(@"/icons/add.png");
            newLauncherButton.AddOnClick(NewLauncherWindow.TryOpenNewWindow);
        }


        public string GetName()
        {
            return PAGE_NAME;
        }
    }
}
