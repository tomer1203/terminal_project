using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerminalProject
{
    public partial class ConfigurationsForm : Form
    {
        public ConfigurationsForm()
        {
            InitializeComponent();

            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            this.setPortcomboBox.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3"});

            this.setBaudratecomboBox.Items.AddRange(new object[] {
            "2400",
            "9600",
            "19200",
            "38400"});

            
        }

        private void saveConfButton_Click(object sender, EventArgs e)
        {

            bool ok = true;
            try
            {
                String port = this.setPortcomboBox.SelectedItem.ToString();
            }
            catch (Exception exception)
            {
                ok = false;
                saveConfErrorLabel.Text = "*Choose Port";
            }

            try
            {
                String baudrate = this.setBaudratecomboBox.SelectedItem.ToString();
            }catch(Exception exception)
            {
                saveConfErrorLabel.Text = "*Choose Baudrate";
                ok = false;
            }
            // TODO: change MCU configurations
            if (ok)
            {
                this.Close();
            }

        }
    }
}
