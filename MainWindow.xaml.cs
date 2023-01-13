using launchspace_desktop.pages;
using launchspace_desktop.windows;
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
    public partial class MainWindow : Window, ITitleBarWindow
    {

        private ControlPage currentPage;
        private WindowActionHandler windowActionHandler;

        public MainWindow()
        {

            InitializeComponent();


         
            this.SetPage(new LaunchersPage());
            titleBar.Init(this, true);
            windowActionHandler = new WindowActionHandler(this, titleBar, titleBarRow);
            this.SizeChanged += (sen, e) => { windowActionHandler.WindowSizeChanged(); };

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
            windowActionHandler.ToggleMaximize();
        }

        public void Minimize()
        {
            windowActionHandler.Minimize();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            //unfocus other elements
            Keyboard.ClearFocus();
        }








        public void SetPage(ControlPage page)
        {
            this.currentPage = page;
            this.pageContent.Content = page;

            //update menu
            mainMenu.PageChanged(page.GetName());
           
        }


        public ControlPage GetPage()
        {
            return this.currentPage;
        }


        /// <summary>
        /// returns the main window to home
        /// </summary>
        public void GoHome()
        {
            SetPage(new LaunchersPage());
        }
     
    }
}
