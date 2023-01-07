using launchspace_compiler.lib.executables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace launchspace_desktop.pages.EditActionPages
{

    /// <summary>
    /// a page which edits the a specific action. 
    /// </summary>
    interface IEditActionPage
    {


        /// <summary>
        /// applys changed to the iexecutable
        /// </summary>
        public void ApplyChanges();

        public void Init(IExecutable exec);
    }


}
