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

namespace launchspace_desktop.components
{
    /// <summary>
    /// Interaction logic for TitleBar.xaml
    /// </summary>
    public partial class TitleBar : UserControl
    {
        public TitleBar()
        {
            InitializeComponent();

            //set title as window title
            titleLabel.Content = App.Current.MainWindow.Title;
            imageButtonClose.SetSource(@"/icons/close.png");
            imageButtonClose.AddOnClick(() =>
            {
                App.Current.MainWindow.Close();
            });
            imageButtonMinimize.SetSource(@"/icons/minimize.png");
            imageButtonMinimize.AddOnClick(() =>
            {
                App.Current.MainWindow.WindowState = WindowState.Minimized;
            });
            imageButtonMaximize.SetSource(@"/icons/maximize.png");
            imageButtonMaximize.AddOnClick(ToggleMaximize);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            App.Current.MainWindow.DragMove();
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            //toggle window mazimization on double click
            ToggleMaximize();

        }

        /// <summary>
        /// toggles the maximization status of the window
        /// </summary>
        private void ToggleMaximize()
        {
            if (App.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                App.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else if (App.Current.MainWindow.WindowState == WindowState.Normal)
            {
                App.Current.MainWindow.WindowState = WindowState.Maximized;
            }
        }
    }
}
