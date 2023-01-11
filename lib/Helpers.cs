using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace launchspace_desktop.lib
{
    internal class Helpers
    {
        /// <summary>
        /// converts a string to the title case and returns it
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToTitleCase(string s)
        {
            TextInfo ti = new CultureInfo("en-US").TextInfo;
            return ti.ToTitleCase(s);
        }


        private static List<(string, string, ImageSource)>  cachedInstalledPrograms;
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>a list of installed programs on the computer (name, path, icon image)</returns>
        public static List<(string, string, ImageSource)> GetInstalledPrograms()
        {

            if(cachedInstalledPrograms != null)
            {
                return cachedInstalledPrograms;
            }

            List<(string, string, ImageSource)> programsOut = new List<(string, string, ImageSource)>();
            var FOLDERID_AppsFolder = new Guid("{1e87508d-89c2-42f0-8a7e-645a0f50ca58}");
            ShellObject appsFolder = (ShellObject)KnownFolderHelper.FromKnownFolderId(FOLDERID_AppsFolder);

            foreach (var app in (IKnownFolder)appsFolder)
            {
                string name = app.Name;
                string appUserModelID = app.ParsingName;
              
                
                ImageSource icon = app.Thumbnail.ExtraLargeBitmapSource;
              
                string path = app.Properties.System.Link.TargetParsingPath.Value;

                if (path != null && path.Trim() != "")
                {
                    programsOut.Add((name, path, icon));
                }

            }

            cachedInstalledPrograms = programsOut;

            return programsOut;
        }
    }
}
