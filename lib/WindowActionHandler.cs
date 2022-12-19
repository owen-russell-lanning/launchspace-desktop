using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace launchspace_desktop.lib
{
    /// <summary>
    /// handles the actions of a window. this includes, minimization and maximization
    /// </summary>
    internal class WindowActionHandler
    {
        Window parent;
        public WindowActionHandler(Window parent)
        {
            this.parent = parent;
        }


        /// <summary>
        /// minimizes the window
        /// </summary>
        public void Minimize()
        {

            App.Current.MainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            App.Current.MainWindow.WindowState = WindowState.Minimized;

        }

    }
}
