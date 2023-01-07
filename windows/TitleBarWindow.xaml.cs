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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace launchspace_desktop.windows
{
    /// <summary>
    /// Interaction logic for TitleBarWindow.xaml
    /// </summary>
    public partial class TitleBarWindow : Window, ITitleBarWindow
    {


        WindowActionHandler windowActionHandler;
        public TitleBarWindow()
        {
            InitializeComponent();
            titleBar.Init(this, false);
            windowActionHandler = new WindowActionHandler(this, titleBar, titleBarRow);
            this.SizeChanged += (sen, e) => { windowActionHandler.WindowSizeChanged(); };
           

        }

        public void SetContent(UIElement content, string title)
        {
            if(this.contentGrid.Children.Count >= 2)
            {
                this.contentGrid.Children.RemoveAt(1);

            }

            Grid.SetRow(content, 1);

            this.contentGrid.Children.Add(content);
            this.Title = title;
            titleBar.RefreshTitle();

         
         

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

        private static bool isOpen = false; //if there is a title bar window open
        private static TitleBarWindow current = null; //current title bar window. null if none

        /// <summary>
        /// trys to open a new launcher window. if there's one open it will not open
        /// </summary>
        public static TitleBarWindow TryOpenNewWindow()
        {
            if (!isOpen)
            {
                current = new TitleBarWindow();
                current.Show();
                isOpen = true;
            }

            return current;
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
