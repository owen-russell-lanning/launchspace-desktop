using launchspace_compiler.lib.executables;
using launchspace_desktop.components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace launchspace_desktop.pages.EditActionPages
{
    /// <summary>
    /// Interaction logic for EditFileExecutablePage.xaml
    /// </summary>
    public partial class EditFileExecutablePage : UserControl, IEditActionPage
    {
        FileExecutable executable;
        BitmapImage highlightedFileImage = new BitmapImage(new Uri(@"/icons/executables/file_highlight.png", UriKind.Relative));
        ImageSource fileImage;
        public EditFileExecutablePage()
        {
            InitializeComponent();


        }
        public void Init(IExecutable exec)
        {
            Init((FileExecutable)exec);
        }

        public void Init(FileExecutable exec)
        {
            this.executable = exec;

            this.pathInput.SetPlaceholder("File Path");
            this.pathInput.SetText(executable.GetPath());

            this.argsInput.SetPlaceholder("File Arguments");

            if (this.executable.GetArgs().Count > 0)
            {
                this.argsInput.SetText(String.Join(" ", executable.GetArgs()));
            }
          
           
        }


        private void pickPathButton_MouseEnter(object sender, MouseEventArgs e)
        {
            fileImage = pickPathButton.Source;
            pickPathButton.Source = highlightedFileImage;
        }

        private void pickPathButton_MouseLeave(object sender, MouseEventArgs e)
        {
            pickPathButton.Source = fileImage;
        }

        private void pickPathButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //open file dialog to choose a file as the new path

            var dialog = new Microsoft.Win32.OpenFileDialog();

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                string filename = dialog.FileName;

                //set as executable path

                pathInput.SetText(filename);
            }
        }

        public void ApplyChanges()
        {

            if (!this.pathInput.IsEmpty())
            {
                this.executable.SetPath(pathInput.Text);
                this.executable.SetArgs(argsInput.Text.Split(" ").ToList());
             
            }
        }


    }
}
