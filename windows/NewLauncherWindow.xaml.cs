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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace launchspace_desktop.windows
{
    /// <summary>
    /// Interaction logic for NewLauncher.xaml
    /// </summary>
    public partial class NewLauncherWindow : Window, TitleBarWindow
    {

        WindowActionHandler windowActionHandler;
        public NewLauncherWindow()
        {
            InitializeComponent();
            titleBar.Init(this, false);
            windowActionHandler = new WindowActionHandler(this, titleBar, titleBarRow);
            this.SizeChanged += (sen, e) => { windowActionHandler.WindowSizeChanged(); };
            nameInput.SetPlaceholder("New Launcher Name");

            createButton.SetText("Create");
            createButton.AddOnClick(CreateButtonOnClick);
           
            
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
                //close window
                this.Close();
            }

            catch (InvalidOperationException e)
            {
                //launcher already exists
                errorLabel.Content = "Launcher with that name already exists";
                return;
            }

            //exit page

        }

        public void Minimize()
        {
            windowActionHandler.Minimize();
        }

        public void ToggleMaximize()
        {
            //no maximization on this window
        }



        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            SetClosed();
            
        }

        private static bool isOpen = false; //if there is a new launcher window open
        private static NewLauncherWindow current = null; //current new launcher window. null if none

        /// <summary>
        /// trys to open a new launcher window. if there's one open it will not open
        /// </summary>
        public static void TryOpenNewWindow()
        {
            if (!isOpen)
            {
                current = new NewLauncherWindow();
                current.Show();
                isOpen = true;
            }
        }

        /// <summary>
        /// sets the current window to be closed. does not actually close the window
        /// </summary>
        protected static void SetClosed()
        {
            isOpen = false;
            current = null;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => WindowStyle = WindowStyle.None));
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            //unfocus other elements
            Keyboard.ClearFocus();
        }


    }
}
