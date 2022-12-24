using launchspace_desktop.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace launchspace_desktop.components
{

    /// <summary>
    /// an image button display for a launcher
    /// </summary>
    internal class LauncherIconButton : Border
    {
        private string launcherName;

        private Grid contentGrid;
        private RowDefinition imageRow;
        private RowDefinition textRow;

        private Label nameLabel;
        private Image launcherImage;

        private List<Action> onClickLs = new List<Action>();

        /// <summary>
        /// creates a launcher button
        /// </summary>
        /// <param name="launcherName">name of launcher</param>
        public LauncherIconButton(string launcherName) : base() 
        {

            this.launcherName = launcherName;
            imageRow = new RowDefinition();
            textRow = new RowDefinition();
            this.contentGrid = new Grid();
            contentGrid.RowDefinitions.Add(imageRow);
            contentGrid.RowDefinitions.Add(textRow);
            this.CornerRadius = new System.Windows.CornerRadius(8);

            //make image
            launcherImage = new Image() { Height = 70};
            launcherImage.Source = new BitmapImage(new Uri(@"/icons/add.png", UriKind.Relative));
            Grid.SetRow(launcherImage, 0);
            contentGrid.Children.Add(launcherImage);

            //make text
            nameLabel = new Label() { Content = launcherName, Foreground = Constants.TEXT_COLOR};
            Grid.SetRow(nameLabel, 1);
            contentGrid.Children.Add(nameLabel);

            this.Child = contentGrid;
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
