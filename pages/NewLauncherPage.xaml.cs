using launchspace_desktop.lib;
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
    /// Interaction logic for NewLauncherPage.xaml
    /// </summary>
    public partial class NewLauncherPage : UserControl
    {
        Window parentWindow;

        public NewLauncherPage()
        {
            InitializeComponent();
            nameInput.SetPlaceholder("New Launcher Name");

            createButton.SetText("Create");
            createButton.AddOnClick(CreateButtonOnClick);

        }

        public void SetWindow(Window parent)
        {
            this.parentWindow = parent;
        }

        /// <summary>
        /// creates a new launcher with given info and closes the page
        /// </summary>
        private void CreateButtonOnClick()
        {
            LauncherManager lm = LauncherManager.Current;
            string name = nameInput.Text;
            if (nameInput.IsEmpty())
            {
                //don't do anything
                return;
            }

            try
            {
                lm.CreateLauncher(name);

                //refresh current page if main window is on launchers page
                MainWindow mainWin = (MainWindow)App.Current.MainWindow;
                if(mainWin.GetPage().GetName() == LaunchersPage.PAGE_NAME)
                {
                    mainWin.SetPage(new LaunchersPage());
                }

                //close window
                if(parentWindow != null)
                {
                    parentWindow.Close();
                }

            }

            catch (InvalidOperationException e)
            {
                //launcher already exists
                errorLabel.Content = "Launcher with that name already exists";
                return;
            }

            //exit page

        }
    }
}
