using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using launchspace_desktop.lib;
using System.Windows;

namespace launchspace_desktop.components
{

  
 

    internal class TextImageButton : Border
    {

        private static readonly SolidColorBrush BACKGROUND_COLOR = Constants.TERTIARY_COLOR;
        private static readonly SolidColorBrush HOVER_COLOR = Constants.TERTIARY_HOVER_COLOR;

        private Image image;
        private Label label;
        private Grid grid;
        private List<Action> onClickLs = new List<Action>(); //list of actions to invoke on click


        public TextImageButton()
        {

            grid = new Grid();
            grid.VerticalAlignment = VerticalAlignment.Center;
            ColumnDefinition c1 = new ColumnDefinition();
            ColumnDefinition c2 = new ColumnDefinition();
            c1.Width = new System.Windows.GridLength(0.2, GridUnitType.Star);
            c2.Width = new System.Windows.GridLength(0.8, GridUnitType.Star);
            grid.ColumnDefinitions.Add(c1);
            grid.ColumnDefinitions.Add(c2);
            image = new Image();
            image.Height = 20;
            image.Margin = new Thickness(10, 0, 0, 0);

            label = new Label();
            label.Foreground = Constants.TEXT_COLOR;
            label.FontSize = 13;
            label.FontWeight = FontWeights.Bold;
            label.Padding = new Thickness(10, 5, 10, 5);
            Grid.SetColumn(image, 0);
            Grid.SetColumn(label, 1);

            grid.Children.Add(image);
            grid.Children.Add(label);

            this.Background = BACKGROUND_COLOR;
            this.Child = grid;
            this.CornerRadius = new CornerRadius(10);
            
       
           
        }

        public void SetText(string text)
        {
            this.label.Content = text;
        }


        public void SetSource(string src)
        {
            image.Source = new BitmapImage(new Uri(src, UriKind.Relative));
        }


        protected override void OnMouseEnter(MouseEventArgs e)
        {

            base.OnMouseEnter(e);
            this.Background = HOVER_COLOR;

        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.Background = BACKGROUND_COLOR;

        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
            foreach (Action a in onClickLs)
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
