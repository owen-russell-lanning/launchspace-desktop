using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace launchspace_desktop.windows
{

    /// <summary>
    /// window with a title bar
    /// </summary>
    public interface TitleBarWindow
    {
        public void Minimize();
        public void ToggleMaximize();
    }
}
