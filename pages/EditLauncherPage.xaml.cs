using launchspace_compiler;
using launchspace_compiler.lib.executables;
using launchspace_desktop.components;
using launchspace_desktop.lib;
using launchspace_desktop.windows;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace launchspace_desktop.pages
{
    /// <summary>
    /// Interaction logic for EditLauncherPage.xaml
    /// </summary>
    public partial class EditLauncherPage : Page, ControlPage
    {
        private static readonly string PAGE_NAME = "Edit Launcher";

        private string launcherName;
        private Queue<IExecutable> execs;
        public EditLauncherPage()
        {
            InitializeComponent();
            editTitle.FontSize = Constants.TITLE_FONT_SIZE;
            editActions.FontSize = Constants.SUBTITLE_FONT_SIZE;
            
            addNewActionButton.SetText("Add Action");
            addNewActionButton.SetSource(@"/icons/add.png");
            addNewActionButton.AddOnClick(AddNewAction);

            changeIconButton.SetText("Change Icon");
            changeIconButton.SetSource(@"/icons/executables/file.png");

            changeIconButton.AddOnClick(PickNewLauncherIcon);

            deleteLauncherButton.SetText("Delete Launcher");
            deleteLauncherButton.SetSource(@"/icons/trash.png");
            deleteLauncherButton.SetHoverColor(Colors.Red);

            deleteLauncherButton.AddOnClick(() =>
            {

                //onclick ask if sure, then delete after next click
                deleteLauncherButton.SetText("Are you sure?");
                deleteLauncherButton.ClearOnClick();
                deleteLauncherButton.AddOnClick(() =>
                {

                    //delete launcher and return to main menu
                    LauncherManager.Current.DeleteLauncher(launcherName);
                    ((MainWindow)App.Current.MainWindow).GoHome();
                });
            });




        }

        /**
         * intiailized the page with the given launcher name
         * */
        public void Init(string launcherName)
        {
            this.launcherName = launcherName;
           

            titleInput.SetPlaceholder("Launcher title");
            titleInput.SetText(Helpers.ToTitleCase(this.launcherName));

            //read launcher into execs
            this.execs = Compiler.ReadIntoExecutables(LauncherManager.Current.GetLauncherFullPath(launcherName));

            //add to action list
            foreach(IExecutable exec in this.execs)
            {
                EditableActionDisplay aDisplay = GetActionDisplay(exec);
                actionStack.Children.Add(aDisplay);
            }

            //get launcher icon
            launcherIcon.Source = LauncherManager.Current.GetLauncherIcon(launcherName);

        }

        private EditableActionDisplay GetActionDisplay(IExecutable exec)
        {
            EditableActionDisplay aDisplay = new EditableActionDisplay(exec);
            aDisplay.AddOnDelete((a) =>
            {
                actionStack.Children.Remove(a);
                List<IExecutable> execLs = execs.ToList();
                execLs.Remove(exec);
                execs = new Queue<IExecutable>(execLs);
                RecompileLauncher();


            });

            aDisplay.AddOnClick((a) =>
            {
                TitleBarWindow newWin = TitleBarWindow.TryOpenNewWindow();
                EditActionPage p = new EditActionPage();
                p.SetWindow(newWin);
                p.Init(a.GetAction());
                newWin.Closed += (sen, e) =>
                {
                    if (p.IsSaved())
                    {

                        //if saved, reload page
                        a.Reload(p.GetAction());
                        List<IExecutable> execLs = execs.ToList();
                        int pos = execLs.IndexOf(exec);
                        if(pos != -1)
                        {
                            execLs.RemoveAt(pos);
                            execLs.Insert(pos, p.GetAction());
                        }
                        execs = new Queue<IExecutable>(execLs);
                        RecompileLauncher();
                    }
                  

                };
                newWin.SetContent(p, "Edit Action");


            });

            return aDisplay;

        }

        public string GetName()
        {
            return PAGE_NAME;
        }

        /// <summary>
        /// adds a new action to the action list on the page
        /// </summary>
        private void AddNewAction()
        {
            //make a new action item
            FileExecutable exec = new FileExecutable("New Action", "", new List<string>()); //make default executable, file exec

            //compile new action into launcher and save launcher
            this.execs.Enqueue(exec);
            RecompileLauncher();

            EditableActionDisplay aDisplay = GetActionDisplay(exec);
            actionStack.Children.Add(aDisplay);
           
        }


        /// <summary>
        /// recompiles and saves the launches with the current saved data
        /// </summary>
        private void RecompileLauncher()
        {
            LauncherManager.Current.CompileLauncher(execs, launcherName);
        }



        /// <summary>
        /// asks the user to pick a new icon for the given image
        /// </summary>
        public void PickNewLauncherIcon()
        {
            //open file dialog to choose a file as the new path

            var dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                string filename = dialog.FileName;
                LauncherManager.Current.SetLauncherIcon(launcherName, filename);

                //refresh icon
                this.launcherIcon.Source = LauncherManager.Current.GetLauncherIcon(launcherName);


            }
        }

    }
}
