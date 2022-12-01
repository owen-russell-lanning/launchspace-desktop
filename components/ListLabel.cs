using launchspace_desktop.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace launchspace_desktop.components
{
    /// <summary>
    /// a label for lists and stack
    /// </summary>
    internal class ListLabel:Label
    {
        public ListLabel()
        {
            this.Foreground = Constants.SECONDARY_TEXT_COLOR;
            this.FontWeight = FontWeights.Bold;
            this.Padding = new Thickness(0, 20, 0, 0);
            this.FontSize = 14;
        }
    }
}
