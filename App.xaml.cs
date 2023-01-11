using launchspace_desktop.lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace launchspace_desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        //init managers
        LauncherManager launcherManager = new LauncherManager();

        public App()
        {
            //load app
            Helpers.GetInstalledPrograms(); //load installed programs into cache
        }

     
    }
}
