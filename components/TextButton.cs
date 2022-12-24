using launchspace_desktop.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace launchspace_desktop.components
{

    /// <summary>
    /// simple text button following similar buttons
    /// </summary>
    public class TextButton : Border
    {


        private static readonly SolidColorBrush BACKGROUND_COLOR = Constants.TERTIARY_COLOR;
        private static readonly SolidColorBrush HOVER_COLOR = Constants.TERTIARY_HOVER_COLOR;


        private Label label;
        private List<Action> onClickLs = new List<Action>(); //list of actions to invoke on click

        public TextButton()
        {
            label = new Label();
            label.Foreground = Constants.TEXT_COLOR;
            label.FontSize = 13;
            label.FontWeight = FontWeights.Bold;
            label.Padding = new Thickness(10, 5, 10, 5);
            this.Child = label;

            this.Background = BACKGROUND_COLOR;
            this.CornerRadius = new CornerRadius(10);
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;



        }
        public void SetText(string text)
        {
            this.label.Content = text;
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
