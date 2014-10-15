using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TicketManager
{
    public class updates
    {
        public string program { get; set; }
        public double version { get; set; }

        public updates(string program, double version)
        {
            this.program = program;
            this.version = version;
        }
    }

    class UpdateManager
    {
        private List<updates> updatesList = new List<updates>();

        public UpdateManager() { }
        
        public void registrerUpdate(string program, double version)
        {
            updatesList.Add(new updates(program, version));
        }

        public bool update()
        {
            List<string> updateUrls = new List<string>();

            foreach (updates u in updatesList)
            {
                string tmp = MainWindow.t4y.lookForUpdate(u.program, u.version);
                if (!string.IsNullOrEmpty(tmp))
                {
                    updateUrls.Add(tmp);
                }
            }

            if (updateUrls.Count != 0)
            {
                try
                {
                    Process p = new Process();
                    p.StartInfo.FileName = "ProgramUpdater.exe";
                    p.StartInfo.Arguments = string.Join(" ", updateUrls);
                    p.Start();
                }
                catch (Exception ex)
                {

                }
                return true;
            }

            return false;
        }
    }
}
