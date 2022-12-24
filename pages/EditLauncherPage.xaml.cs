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
    /// Interaction logic for EditLauncherPage.xaml
    /// </summary>
    public partial class EditLauncherPage : Page, ControlPage
    {
        private static readonly string PAGE_NAME = "Edit Launcher";

        private string launcherName;
        public EditLauncherPage()
        {
            InitializeComponent();
        }

        /**
         * intiailized the page with the given launcher name
         * */
        public void Init(string launcherName)
        {
            this.launcherName = launcherName;
        }

        public string GetName()
        {
            return PAGE_NAME;
        }
    }
}
