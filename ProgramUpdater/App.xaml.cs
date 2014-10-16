using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
    using System.Windows;

namespace ProgramUpdater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Args[0]))
                {
                    StaticItems.updateDownloadUrl = e.Args[0];
                }
            }
            catch (Exception ex) { }

            base.OnStartup(e);
        }
    }
}
