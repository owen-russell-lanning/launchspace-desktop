using IWshRuntimeLibrary;
using launchspace_compiler;
using launchspace_compiler.lib.executables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;


namespace launchspace_desktop.lib
{

    /// <summary>
    /// manages existing launchers. interacts with the compiler
    /// </summary>
    class LauncherManager
    {

        private static readonly string LAUNCHERS_PATH = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "launchspace", "launchers");
        private static readonly string LAUNCHERS_START_MENU_PATH = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs", "launchspace", "launchers");
        private static readonly string ICONS_PATH = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "launchspace", "icons");
    
        public static LauncherManager Current { get; private set; }



        public LauncherManager()
        {
            Current = this;

            //create launchers path if it dosen't exist
            if (!Directory.Exists(LAUNCHERS_PATH))
            {
                Directory.CreateDirectory(LAUNCHERS_PATH);
            }
            if (!Directory.Exists(LAUNCHERS_START_MENU_PATH))
            {
                Directory.CreateDirectory(LAUNCHERS_START_MENU_PATH);
            }
            if (!Directory.Exists(ICONS_PATH))
            {
                Directory.CreateDirectory(ICONS_PATH);
            }

          
            



        }


        /// <summary>
        /// creates a new launcher in the default path
        /// </summary>
        /// <param name="name">name of the new launcher</param>
        public void CreateLauncher(string name)
        {
            string newPath = Path.Join(LAUNCHERS_PATH, name + ".lnch");
            if (System.IO.File.Exists(newPath))
            {
                throw new InvalidOperationException("Cannot create new launcher \"" + name + ".lnch\" Launcher Already Exists");
            }

            //file dosen't exist write new file
            System.IO.File.WriteAllText(newPath, "[]");

            //create shortcut for new launcher
            CreateLauncherShortcut(name, newPath);
        }

        private void CreateLauncherShortcut(string name, string shortcutEndpointPath)
        {


            CreateLauncherShortcut(name, shortcutEndpointPath, "");
        }


        /// <summary>
        /// creates a shortcut for a given launcher
        /// </summary>
        private void CreateLauncherShortcut(string name, string shortcutEndpointPath, string iconPath)
        {


            string filePath = GetLauncherShortcutPath(name);

            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(filePath);
            shortcut.Description = "launchspace shortcut";
            shortcut.TargetPath = shortcutEndpointPath;

            if (iconPath.Trim() != "")
            {
                shortcut.IconLocation = iconPath;
            }
            shortcut.Save();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>a list of launcher names in the launcher path</returns>
        public List<string> GetLauncherNames()
        {
            string[] files = Directory.GetFiles(LAUNCHERS_PATH);
            List<string> launchers = new List<string>();
            //filter out non .lnch files
            foreach (string file in files)
            {
                if (file.EndsWith(".lnch"))
                {

                    //remove .lnchs and front of path
                    string newName = file.Substring(0, file.Length - 5);
                    launchers.Add(Path.GetFileName(newName));

                }
            }

            return launchers;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>the full path of a launher's shortcut file</returns>
        public string GetLauncherShortcutPath(string name)
        {
            return Path.Join(LAUNCHERS_START_MENU_PATH, name + ".lnk");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>the full path of the launcher with the given name</returns>
        public string GetLauncherFullPath(string name)
        {
            return Path.Join(LAUNCHERS_PATH, name + ".lnch");
        }

        public void CompileLauncher(Queue<IExecutable> execs, string name)
        {
            Compiler.Compile(execs, GetLauncherFullPath(name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>the image source for the launcher</returns>
        public ImageSource GetLauncherIcon(string name)
        {

            string imgPath = Path.Join(ICONS_PATH, name + ".ico");
            if (System.IO.File.Exists(imgPath))
            {
                return (ImageSource)(new BitmapImage(new Uri(imgPath, UriKind.Absolute)));
            }

            return Helpers.GetExecutableIcon(GetLauncherShortcutPath(name));
        }


        /// <summary>
        /// changes the launcher's shortcut icon. will not change executable icon
        /// </summary>
        /// <param name="imagePath">path of image to change icon to</param>
        public void SetLauncherIcon(string name, string imagePath)
        {
            string icoPath = Path.Join(ICONS_PATH, name + ".ico");

            //convert image to ico and copy to local folder
            using (FileStream stream = System.IO.File.OpenWrite(icoPath))
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(imagePath);
                Icon.FromHandle(bitmap.GetHicon()).Save(stream);
            }
        

            if(!System.IO.File.Exists(icoPath)){
                return;
            }


            //delete old shortcut and create new shortcut
            string shPath = GetLauncherShortcutPath(name);
            System.IO.File.Delete(shPath);

           
            CreateLauncherShortcut(name, GetLauncherFullPath(name), icoPath);


        }




        /// <summary>
        /// deletes the launcher with the given name
        /// </summary>
        /// <param name="name">name of the launcher to delete</param>
        public void DeleteLauncher(string name)
        {
            try
            {
                //delete lnch file and shortcut
                string absPath = GetLauncherFullPath(name);
                System.IO.File.Delete(absPath);

                string shPath = GetLauncherShortcutPath(name);
                System.IO.File.Delete(shPath);

                //delete cached image
                string imgPath = Path.Join(ICONS_PATH, name + ".png");
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("[LauncherManager] Unable to delete launcher \"" + name + "\"");
                Debug.WriteLine(e);
            }


        }










    }
}
