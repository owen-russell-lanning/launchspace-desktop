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


        /// <summary>
        /// title bar with input. does not need to be initialized
        /// </summary>
        /// <param name="parent"></param>
        public TitleBar(Window parent)
        {
            InitializeComponent();
            Init(parent);

           
        }

        /// <summary>
        /// call init after using paremeterless constructor
        /// </summary>
        public TitleBar()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// initializes the title bar
        /// </summary>
        /// <param name="parent"></param>
        public void Init(Window parent)
        {
            this.parent = parent;
            //set title as window title
            titleLabel.Content = parent.Title;
            imageButtonClose.SetSource(@"/icons/close.png");
            imageButtonClose.AddOnClick(() =>
            {
                parent.Close();
            });
            imageButtonMinimize.SetSource(@"/icons/minimize.png");
            imageButtonMinimize.AddOnClick(((MainWindow)App.Current.MainWindow).Minimize);
            imageButtonMaximize.SetSource(@"/icons/maximize.png");
            imageButtonMaximize.AddOnClick(((MainWindow)App.Current.MainWindow).ToggleMaximize);
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
            ((MainWindow)App.Current.MainWindow).ToggleMaximize();

        }






    }
}
