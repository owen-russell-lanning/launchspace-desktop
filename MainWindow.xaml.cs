using launchspace_desktop.lib;
using launchspace_desktop.pages;
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
using System.Windows.Shell;
using System.Windows.Threading;

namespace launchspace_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double normalHeight;
        private double normalWidth;
        private double normalTop;
        private double normalLeft;
        private bool wasMaximized = false;
        private bool firstMaximizeResize = false; //first resze from the window in the maximzation process
        private bool isMaximizedWithTaskbar = false; //true if the window is maximized with taskbar
        private ControlPage currentPage;
        private WindowActionHandler windowActionHandler;

        public MainWindow()
        {

            InitializeComponent();

            this.normalHeight = Height;
            this.normalWidth = Width;
            this.SizeChanged += (sen, e) => { HandleSizeChanged(); };
            this.SetPage(new LaunchersPage());
            titleBar.Init(this);
            windowActionHandler = new WindowActionHandler(this);

        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => WindowStyle = WindowStyle.None));
        }


        /// <summary>
        /// toggles the maximization status of the window
        /// </summary>
        public void ToggleMaximize()
        {
            if (isMaximizedWithTaskbar)
            {
                HandleSizeChanged();
            }
            else if (App.Current.MainWindow.WindowState == WindowState.Maximized)
            {

               
                App.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else if (App.Current.MainWindow.WindowState == WindowState.Normal)
            {


    
                App.Current.MainWindow.WindowState = WindowState.Maximized;

            }
        }

        public void Minimize()
        {
            windowActionHandler.Minimize();
        }

    


        /// <summary>
        /// handles changing of the window size
        /// </summary>
        private void HandleSizeChanged()
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
                    this.Width = normalWidth;
                    this.Height = normalHeight;
                    this.Top = normalTop;
                    this.Left = normalLeft;
                }
                else
                {
                    normalWidth = this.ActualWidth;
                    normalHeight = this.ActualHeight;
                    normalLeft = this.Left;
                    normalTop = this.Top;
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
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                //get window size
                double fullHeight = this.ActualHeight;
                double fullWidth = this.ActualWidth;


                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.None;

                this.Height = fullHeight;
                this.Width = fullWidth - 15;

                this.Top = 0;
                this.Left = 0;
                isMaximizedWithTaskbar = true;
               
             


            }
            else
            {
                wasMaximized = false;
            }



        }

        public void SetPage(ControlPage page)
        {
            this.currentPage = page;
            this.pageContent.Content = page;

            //update menu
            mainMenu.PageChanged(page.GetName());
           
        }
     
    }
}
