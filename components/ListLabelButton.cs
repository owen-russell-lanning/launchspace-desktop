using launchspace_desktop.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Input;

namespace launchspace_desktop.components
{
    internal class ListLabelButton : ListLabel
    {

        bool toggled = false;
 

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.Foreground = Constants.TEXT_COLOR;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (!toggled)
            {
                base.OnMouseLeave(e);
                this.Foreground = Constants.SECONDARY_TEXT_COLOR;
            }
        }

        public void ToggleOn()
        {
            toggled = true;
            this.Foreground = Constants.TEXT_COLOR;
        }

        public void ToggleOff()
        {
            toggled = false;
            this.Foreground = Constants.SECONDARY_TEXT_COLOR;

        }

      
    }

    
}
