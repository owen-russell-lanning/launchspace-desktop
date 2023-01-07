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

namespace launchspace_desktop.components
{
    /// <summary>
    /// Interaction logic for TitleBar.xaml
    /// </summary>
    public partial class TitleBar : UserControl
    {
        Window parent = null; //the window the title bar is on
        bool maximizeable = false; //if the window can be maximized


        /// <summary>
        /// title bar with input. does not need to be initialized
        /// </summary>
        /// <param name="parent"></param>
        public TitleBar(ITitleBarWindow parent, bool maximizeable)
        {
            InitializeComponent();
            Init(parent, maximizeable);

           
        }

        /// <summary>
        /// call init after using paremeterless constructor
        /// </summary>
        public TitleBar()
        {
            InitializeComponent();
            maximizeable = true;
            
        }

        public void RefreshTitle()
        {
            titleLabel.Content = this.parent.Title;
        }

        /// <summary>
        /// initializes the title bar
        /// </summary>
        /// <param name="parent"></param>
        public void Init(ITitleBarWindow parent, bool maximizeable)
        {
            this.parent = (Window)parent;
            this.maximizeable = maximizeable;
            //set title as window title
            RefreshTitle();
            imageButtonClose.SetSource(@"/icons/close.png");
            imageButtonClose.AddOnClick(() =>
            {
                this.parent.Close();
            });
            imageButtonMinimize.SetSource(@"/icons/minimize.png");
            imageButtonMinimize.AddOnClick(((ITitleBarWindow)parent).Minimize);

            if (maximizeable)
            {
                imageButtonMaximize.SetSource(@"/icons/maximize.png");
                imageButtonMaximize.AddOnClick(((ITitleBarWindow)parent).ToggleMaximize);
            }
            else
            {

                //remove maximize button and move minimize
                Grid.SetColumn(imageButtonMinimize, 2);
                grid.Children.Remove(imageButtonMaximize);
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            parent.DragMove();
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            //toggle window mazimization on double click
            ((ITitleBarWindow)parent).ToggleMaximize();

        }






    }
}
