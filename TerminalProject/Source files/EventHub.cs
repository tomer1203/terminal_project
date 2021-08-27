using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalProject.Source_files
{
    public static class EventHub
    {
        /////////////////////////////////
        // Save Serial Configurations
        ////////////////////////////////
        public static event EventHandler<EventArgs> SaveSerialConfigurationsHandler;

        public static void OnSaveSerialConfigurations(object sender, EventArgs e)
        {
            SaveSerialConfigurationsHandler?.Invoke(sender, e);
        }

        /////////////////////////////////
        // Save File Configurations
        ////////////////////////////////
        public static event EventHandler<EventArgs> SaveFileConfigurationsHandler;

        public static void OnSaveFileConfigurations(object sender, EventArgs e)
        {
            SaveFileConfigurationsHandler?.Invoke(sender, e);
        }

        ////////////////////////////////////
        // File Sending Progress Handler
        ///////////////////////////////////
        public static event EventHandler<EventArgs> fileSendingProgressHandler;

        public static void OnFileSendingProgress(object sender, EventArgs e)
        {
            fileSendingProgressHandler?.Invoke(sender, e);
        }

    }
}
