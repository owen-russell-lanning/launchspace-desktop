
using launchspace_desktop.lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace launchspace_desktop.components
{
    class HoverableImageButton : Grid
    {
      
        private Image image;
        private List<Action> onClickLs = new List<Action>(); //list of actions to invoke on click


        public HoverableImageButton()
        {
            image = new Image();
            this.Children.Add(image);
            

       
        }

        public void SetSource(string src)
        {
            image.Source = new BitmapImage(new Uri(src, UriKind.Relative));
        }


        protected override void OnMouseEnter(MouseEventArgs e)
        {

            base.OnMouseEnter(e);
            this.Background = Constants.TERTIARY_COLOR; 
            
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.Background = new SolidColorBrush(Colors.Transparent);

        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
            foreach(Action a in onClickLs)
            {
                a.Invoke();
            }
        }

        public void AddOnClick(Action a)
        {
            onClickLs.Add(a);
        }
    }

 
}
