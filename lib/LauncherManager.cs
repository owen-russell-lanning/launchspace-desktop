using launchspace_compiler;
using launchspace_compiler.lib.executables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace launchspace_desktop.lib
{

    /// <summary>
    /// manages existing launchers. interacts with the compiler
    /// </summary>
    class LauncherManager
    {

        private static readonly string LAUNCHERS_PATH = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "launchspace", "launchers");
   
        public static LauncherManager Current { get; private set; }



        public LauncherManager()
        {
            Current = this;

            //create launchers path if it dosen't exist
            if (!Directory.Exists(LAUNCHERS_PATH))
            {
                Directory.CreateDirectory(LAUNCHERS_PATH);
            }
        }


        /// <summary>
        /// creates a new launcher in the default path
        /// </summary>
        /// <param name="name">name of the new launcher</param>
        public void CreateLauncher(string name) 
        {
            string newPath = Path.Join(LAUNCHERS_PATH, name + ".lnch");
            if (File.Exists(newPath))
            {
                throw new InvalidOperationException("Cannot create new launcher \"" + name + ".lnch\" Launcher Already Exists");
            }

            //file dosen't exist write new file
            File.WriteAllText(newPath, "[]");
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
            foreach(string file in files)
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
        /// <returns>the full path of the launcher with the given name</returns>
        public string GetLauncherFullPath(string name)
        {
            return Path.Join(LAUNCHERS_PATH, name + ".lnch");
        }

        public void CompileLauncher(Queue<IExecutable> execs, string name)
        {
            Compiler.Compile(execs, GetLauncherFullPath(name));
        }


        


    }
}
