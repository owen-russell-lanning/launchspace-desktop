using launchspace_compiler.lib.executables;
using launchspace_desktop.components;
using launchspace_desktop.lib;
using launchspace_desktop.pages.EditActionPages;
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
    /// Interaction logic for EditActionPage.xaml
    /// </summary>
    public partial class EditActionPage : UserControl
    {
        Window parentWindow;
        IExecutable action;

        BitmapImage highlightedFileImage = new BitmapImage(new Uri(@"/icons/executables/file_highlight.png", UriKind.Relative));
        ImageSource fileImage;
        BitmapImage highlightedProgramImage = new BitmapImage(new Uri(@"/icons/executables/program_highlight.png", UriKind.Relative));
        ImageSource programImage;

        IEditActionPage subPage;

        private bool saved = false;

        public EditActionPage()
        {
            InitializeComponent();
        }

        public void Init(IExecutable exec)
        {
            this.action = exec;

            saveButton.SetText("Save");
            saveButton.AddOnClick(SaveButtonOnClick);
            SetSubPage();

            fileImage = fileIcon.Source;
            programImage = programIcon.Source;

            fileIcon.AddOnClick(() =>
            {
                action = FileExecutable.GetDefault();
                HighlightSelectedAction();
                SetSubPage();
            });
            programIcon.AddOnClick(() =>
            {
                action = ProgramExecutable.GetDefault();
                HighlightSelectedAction();
                SetSubPage();
            });


            HighlightSelectedAction();


        }

        public void SetWindow(Window parent)
        {
            this.parentWindow = parent;
        }

        public IExecutable GetAction()
        {
            return this.action;
        }


        /// <summary>
        /// sets the sub page based on the action
        /// </summary>
        private void SetSubPage()
        {


            
            switch (this.action.GetExecutableTypeId())
            {
                case FileExecutable.EXECUTABLE_TYPE_ID:
                    subPage = new EditFileExecutablePage();
                    subPage.Init(this.action);
                    break;
                case ProgramExecutable.EXECUTABLE_TYPE_ID:
                    subPage = new EditProgramExecutablePage();
                    subPage.Init(this.action);
                    break;
                default:
                    throw new KeyNotFoundException("Executable type \"" + this.action.GetName() + "\" not found");
            }


            if (grid.Children.Count > 2)
            {
                grid.Children.RemoveAt(2);
            }

            Grid.SetRow((UIElement)subPage, 1);
            grid.Children.Add((UIElement)subPage);
        }

        private void SaveButtonOnClick()
        {

            //apply changes on page
            subPage.ApplyChanges();
            saved = true;

            //close window
            parentWindow.Close();
        }

        public bool IsSaved()
        {
            return saved;
        }

        /// <summary>
        /// highlights the icon for the selected action type and normalizes the rest
        /// </summary>
        private void HighlightSelectedAction()
        {
            int selected = action.GetExecutableTypeId();


            fileIcon.Source = fileImage;
            programIcon.Source = programImage;

            if (selected == FileExecutable.EXECUTABLE_TYPE_ID)
            {
                fileIcon.Source = highlightedFileImage;
            }
            else if (selected == ProgramExecutable.EXECUTABLE_TYPE_ID)
            {
                programIcon.Source = highlightedProgramImage;
            }
        }

    }
}
