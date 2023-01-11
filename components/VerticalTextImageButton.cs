using launchspace_desktop.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace launchspace_desktop.components
{
    internal class VerticalTextImageButton : Grid
    {
        private ImageSource source;
        private string text;
        private RowDefinition imgRow;
        private RowDefinition txtRow;
        private Image img;
        private TextBlock txt;

        private List<Action> onClickLs = new List<Action>();
        public VerticalTextImageButton(string text, ImageSource source) : base()
        {
            this.source = source;
            this.text = text;

            //create image grid
            imgRow = new RowDefinition() { Height = new System.Windows.GridLength(0.6, System.Windows.GridUnitType.Star)};
            txtRow = new RowDefinition() { Height = new System.Windows.GridLength(0.4, System.Windows.GridUnitType.Star) };
            this.RowDefinitions.Add(imgRow);
            this.RowDefinitions.Add(txtRow);

            //create image
            img = new Image();
            img.Source = source;
            this.Children.Add(img);

            //create text
            this.txt = new TextBlock();
            Grid.SetRow(txt, 1);
            this.txt.Text = text;
            this.txt.FontSize = 10;
            this.txt.Foreground = Constants.TEXT_COLOR;
            this.txt.TextWrapping = System.Windows.TextWrapping.Wrap;
            this.txt.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.Children.Add(txt);


        }

        public void AddOnClick(Action a)
        {
            this.onClickLs.Add(a);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            foreach(Action a in onClickLs)
            {
                a.Invoke();
            }
        }
    }
}
