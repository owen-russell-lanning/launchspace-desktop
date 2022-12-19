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

namespace launchspace_desktop.windows
{
    /// <summary>
    /// Interaction logic for NewLauncher.xaml
    /// </summary>
    public partial class NewLauncherWindow : Window
    {
        public NewLauncherWindow()
        {
            InitializeComponent();
            titleBar.Init(this);
            
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


    }
}
