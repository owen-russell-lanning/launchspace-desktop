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
    
            
        }

        private EditableActionDisplay GetActionDisplay(IExecutable exec)
        {
            EditableActionDisplay aDisplay = new EditableActionDisplay(exec);
            aDisplay.AddOnDelete((a) =>
            {
                actionStack.Children.Remove(a);

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

     
    }
}
