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

  
 

    internal class HorizontalTextImageButton : Border
    {

        private static readonly SolidColorBrush BACKGROUND_COLOR = Constants.TERTIARY_COLOR;
       
        private static readonly SolidColorBrush TOGGLED_COLOR = Constants.HIGHLIGHT_COLOR;

        private Image image;
        private Label label;
        private Grid grid;
        private List<Action> onClickLs = new List<Action>(); //list of actions to invoke on click
        private bool toggleable = false; //if the button is a toggleable button
        private bool toggled = false;

        private SolidColorBrush hoverColor = Constants.HIGHLIGHT_COLOR;


        public HorizontalTextImageButton()
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

        public void MakeToggleable()
        {
            this.toggleable = true;
        }

        public bool IsToggled()
        {
            return this.toggled;
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
            this.Background = hoverColor;

            if (this.toggled)
            {
                this.Background = TOGGLED_COLOR;
            }


        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.Background = BACKGROUND_COLOR;

            if (this.toggled)
            {
                this.Background = TOGGLED_COLOR;
            }

        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {

            if (this.toggleable)
            {
                this.toggled = !this.toggled;
                if (this.toggled)
                {
                    this.Background = TOGGLED_COLOR;
                }
                else
                {
                    this.Background = hoverColor;
                }
            }

            e.Handled = true;
            for(var i =0; i< onClickLs.Count; i++)
            {
                onClickLs[i].Invoke();
            }

        }

        public void AddOnClick(Action a)
        {
            onClickLs.Add(a);
        }

        public void SetHoverColor(Color c)
        {
            this.hoverColor = new SolidColorBrush(c);
        }


        /// <summary>
        /// clears all on click actions
        /// </summary>
        public void ClearOnClick()
        {
            onClickLs.Clear();
        }
    }


}
