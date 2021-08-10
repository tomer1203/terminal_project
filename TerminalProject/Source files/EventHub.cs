﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalProject.Source_files
{
    public static class EventHub
    {
        public static event EventHandler<EventArgs> saveConfigurationsHandler;

        public static void OnSaveConfigurations(object sender, EventArgs e)
        {
            saveConfigurationsHandler?.Invoke(sender, e);
        }
    }
}