using launchspace_compiler.lib.executables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace launchspace_desktop.pages.EditActionPages
{
    /// <summary>
    /// Interaction logic for EditProgramExecutablePage.xaml
    /// </summary>
    public partial class EditProgramExecutablePage : UserControl, IEditActionPage
    {
        private ProgramExecutable exec;

        public EditProgramExecutablePage()
        {
            InitializeComponent();
        }

        public void ApplyChanges()
        {
            throw new NotImplementedException();
        }

        public void Init(IExecutable exec)
        {
            Init((ProgramExecutable)exec);
        }

        public void Init(ProgramExecutable exec)
        {
            this.exec = exec;
        }
    }
}
