using launchspace_compiler;
using launchspace_desktop.components;
using launchspace_desktop.lib;
using launchspace_desktop.windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private bool editMode = false; //if page is currently in edit mode

        public LaunchersPage()
        {
            InitializeComponent();
            newLauncherButton.SetText("New Launcher");
            newLauncherButton.SetSource(@"/icons/add.png");
            newLauncherButton.AddOnClick(() =>
            {
                //open a sub window with a new launcher page as it's content
                TitleBarWindow newWin = TitleBarWindow.TryOpenNewWindow();
                NewLauncherPage p = new NewLauncherPage();
                newWin.SetContent(p, "New Launcher");
                p.SetWindow(newWin);
            });

            editModeButton.SetText("Edit");
            editModeButton.SetSource(@"/icons/edit.png");
            editModeButton.Margin = new Thickness(10, 0, 10, 0);
            editModeButton.MakeToggleable();
            editModeButton.AddOnClick(() =>
            {
                if (editModeButton.IsToggled())
                {
                    editMode = true;
                }
                else
                {
                    editMode = false;
                }
            });


  

            PopulateLaunchers();

        }

        /// <summary>
        /// populates launchers from the manager onto the page
        /// </summary>
        private void PopulateLaunchers()
        {
            //get all launchers
            List<string> launchers = LauncherManager.Current.GetLauncherNames();
            launchersDisplay.Children.Clear();

            if (launchers.Count == 0) //set no launchers message
            {

                ListLabel noLaunchersMessage = new ListLabel() { Content = "No Launchers Found" };
                launchersDisplay.Children.Add(noLaunchersMessage);
                return;

            }

            //display launchers
            foreach (string launcher in launchers)
            {
                try
                {
                    LauncherIconButton lButton = new LauncherIconButton(launcher);
                    lButton.Margin = new Thickness(10);
                    launchersDisplay.Children.Add(lButton);
                    lButton.AddOnClick(() =>
                    {
                        if (editMode)
                        {
                            //open edit page
                            EditLauncherPage elp = new EditLauncherPage();
                            elp.Init(launcher);
                            ((MainWindow)App.Current.MainWindow).SetPage(elp);
                        }
                        else
                        {
                            //run launcher
                            Compiler.ReadAndExecute(LauncherManager.Current.GetLauncherFullPath(launcher));
                        }
                    });
                }
                catch(FileNotFoundException e)
                {
                    Debug.WriteLine("[LaunchersPage] Unable to populate launcher \"" + launcher + "\"");
                }
            }


        }


        public string GetName()
        {
            return PAGE_NAME;
        }
    }
}
