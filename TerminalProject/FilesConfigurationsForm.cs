using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TerminalProject.Source_files;

namespace TerminalProject
{
    public partial class FilesConfigurationsForm : Form
    {

        /*
         * Constructor
         */
        public FilesConfigurationsForm(string filesToSendDirectory)
        {
            InitializeComponent();

            this.setFilesLibraryTextBox.Text = filesToSendDirectory;
        }

        /*
         * Save Files Library Path Button
         */
        private void saveFilesLibraryButton_Click(object sender, EventArgs e)
        {

            string filesToSendDirectory = setFilesLibraryTextBox.Text;

            if (filesToSendDirectory.Equals(""))
            {
                saveConfErrorLabel.Text = "directory path canot be empty";
                return;
            }


            EventHub.OnSaveFileConfigurations(filesToSendDirectory, EventArgs.Empty);
            this.Close();
        }


        /*
        * Listen to EnterKey to send data
        */
        private void saveConfButton_KeyDown(Object sender, KeyEventArgs keyEvent)
        {
            if (keyEvent.KeyCode == Keys.Enter)
            {
                saveFilesLibraryButton_Click(sender, new EventArgs());
            }
        }


    }
}
