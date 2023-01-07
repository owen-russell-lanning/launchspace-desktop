using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace launchspace_desktop.components
{

    /// <summary>
    /// images with click functionality
    /// </summary>
    public class ImageButton : Image
    {
        private List<Action> onClickLs = new List<Action>();

        public ImageButton() : base() {
         
        }

        public void AddOnClick(Action a)
        {
            onClickLs.Add(a);
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
