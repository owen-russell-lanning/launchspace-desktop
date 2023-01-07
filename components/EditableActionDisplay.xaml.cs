using launchspace_compiler.lib.executables;
using launchspace_desktop.lib;
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

namespace launchspace_desktop.components
{
    /// <summary>
    /// Interaction logic for EditableActionDisplay.xaml
    /// </summary>
    public partial class EditableActionDisplay : UserControl
    {

        private IExecutable action;
        private static readonly string EXECUTABLE_ICON_PATH = @"/icons/executables";
        private List<Action<EditableActionDisplay>> onDeleteLs = new List<Action<EditableActionDisplay>>();
        private List<Action<EditableActionDisplay>> onClickLs = new List<Action<EditableActionDisplay>>();

        /// <summary>
        /// displays a launcher action and allows for editing of action. call init after setting action
        /// </summary>
        public EditableActionDisplay()
        {
            InitializeComponent();
        }



        public EditableActionDisplay(IExecutable action)
        {
            InitializeComponent();
            this.action = action;
            Init();
        }

        public void SetAction(IExecutable action)
        {
            this.action = action;
        }

        public IExecutable GetAction()
        {
            return this.action;
        }

        /// <summary>
        /// intializes the component once inputs have been set
        /// </summary>
        private void Init()
        {
            this.name.Text = this.action.GetName();

            //set image
            string imgPath = GetExecutableIcon(this.action.GetExecutableTypeId());
            this.icon.Source = new BitmapImage(new Uri(imgPath, UriKind.Relative));



        }

        private void SubOnMouseEnter(object sender, MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            border.Background = Constants.HIGHLIGHT_COLOR;
        }

        private void SubOnMouseLeave(object sender, MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            border.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void SubOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            foreach(Action<EditableActionDisplay> a in onClickLs)
            {
                a.Invoke(this);
            }
        }

        private void DeleteOnMouseEnter(object sen, MouseEventArgs e)
        {
            e.Handled = true;
            ((Border)sen).Background = new SolidColorBrush(Colors.Red);
        }

        private void DeleteOnMouseLeave(object sen, MouseEventArgs e)
        {

            e.Handled = true;
            ((Border)sen).Background = new SolidColorBrush(Colors.Transparent);
        }

        private void Delete(object sen, MouseEventArgs e)
        {
            foreach (Action<EditableActionDisplay> a in onDeleteLs)
            {
                a.Invoke(this);
            }
        }

        public void AddOnDelete(Action<EditableActionDisplay> a)
        {
            this.onDeleteLs.Add(a);
        }

        public void AddOnClick(Action<EditableActionDisplay> a)
        {
            this.onClickLs.Add(a);
        }

        public void Reload(IExecutable newAction)
        {
            this.action = newAction;
            this.Init();
        }

        /// <summary>
        /// gets the correct icon path for the given launchspace-compiler executable type
        /// </summary>
        /// <param name="execType">type of executable</param>
        /// <returns>path to correct icon</returns>
        public static string GetExecutableIcon(int execType)
        {
            switch (execType)
            {

                case FileExecutable.EXECUTABLE_TYPE_ID:

                    //file executable icon
                    return Path.Join(EXECUTABLE_ICON_PATH, "file.png");
                case ProgramExecutable.EXECUTABLE_TYPE_ID:
                    //program executable icon
                    return Path.Join(EXECUTABLE_ICON_PATH, "program.png");
                default:
                    throw new KeyNotFoundException("Executable type \"" + execType + "\" not found");
            }
        }
    }
}
