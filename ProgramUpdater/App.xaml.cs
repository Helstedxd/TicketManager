﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

            foreach (string arg in e.Args)
            {

                MessageBox.Show(arg);
                // TODO: whatever

            }

            base.OnStartup(e);

        }
    }
}
