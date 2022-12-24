using launchspace_desktop.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace launchspace_desktop.windows
{
    /// <summary>
    /// handles the actions of a window with no default title bar. this includes, minimization and maximization
    /// </summary>
    internal class WindowActionHandler
    {
        Window parent;
        bool isMaximizedWithTaskbar = false;
        bool firstMaximizeResize = false;
        private double normalHeight;
        private double normalWidth;
        private double normalTop;
        private double normalLeft;
        private bool wasMaximized = false;
        TitleBar titleBar; //window's titlebar
        RowDefinition titleBarRow; //row of the window's titlebar
        public WindowActionHandler(Window parent, TitleBar titleBar, RowDefinition titleBarRow)
        {
            this.parent = parent;
            this.titleBar = titleBar;
            this.titleBarRow = titleBarRow;
            this.normalHeight = parent.Height;
            this.normalWidth = parent.Width;
        }



        /// <summary>
        /// minimizes the window
        /// </summary>
        public void Minimize()
        {

            parent.WindowStyle = WindowStyle.SingleBorderWindow;
            parent.WindowState = WindowState.Minimized;

        }


        /// <summary>
        /// toggles if the window is maximized
        /// </summary>
        public void ToggleMaximize()
        {
            if (isMaximizedWithTaskbar)
            {
                WindowSizeChanged();
            }
            else if (parent.WindowState == WindowState.Maximized)
            {


                parent.WindowState = WindowState.Normal;
            }
            else if (parent.WindowState== WindowState.Normal)
            {



                parent.WindowState = WindowState.Maximized;

            }
        }


        /// <summary>
        /// handles the change of the parent's window size
        /// </summary>
        public void WindowSizeChanged()
        {
            if (firstMaximizeResize)
            {
                firstMaximizeResize = false;
                return;
            }


            if (App.Current.MainWindow.WindowState == WindowState.Normal)
            {
                //remove fullscreen properties
                this.titleBarRow.Height = new GridLength(30);
                this.titleBar.Padding = new Thickness(0);
                if (wasMaximized)
                {
                    parent.Width = normalWidth;
                    parent.Height = normalHeight;
                    parent.Top = normalTop;
                    parent.Left = normalLeft;
                }
                else
                {
                    normalWidth = parent.ActualWidth;
                    normalHeight = parent.ActualHeight;
                    normalLeft = parent.Left;
                    normalTop = parent.Top;
                }
                wasMaximized = false;

                isMaximizedWithTaskbar = false;

            }
            else if (App.Current.MainWindow.WindowState == WindowState.Maximized)
            {


                firstMaximizeResize = true;
                wasMaximized = true;
                //add fullscreen properties
                this.titleBarRow.Height = new GridLength(50);
                this.titleBar.Padding = new Thickness(15, 0, 15, 0);
                parent.WindowStyle = WindowStyle.SingleBorderWindow;
                //get window size
                double fullHeight = parent.ActualHeight;
                double fullWidth = parent.ActualWidth;


                parent.WindowState = WindowState.Normal;
                parent.WindowStyle = WindowStyle.None;

                parent.Height = fullHeight;
                parent.Width = fullWidth - 15;

                parent.Top = 0;
                parent.Left = 0;
                isMaximizedWithTaskbar = true;




            }
            else
            {
                wasMaximized = false;
            }

        }


    }
}
