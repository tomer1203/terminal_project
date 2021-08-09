using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;

namespace TerminalProject
{
    public partial class ConfigurationsForm : Form
    {
        private SerialPort mSerialPort;

        public ConfigurationsForm(ref SerialPort serialPort)
        {


            InitializeComponent();

            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            this.setPortcomboBox.Items.AddRange(SerialPort.GetPortNames());

            this.setBaudratecomboBox.Items.AddRange(new object[] {
            "2400",
            "9600",
            "19200",
            "38400"});

            
        }

        private void saveConfButton_Click(object sender, EventArgs e)
        {
            bool ok = true;
            String port = "COM1";
            int baudrate = 9600;
            try
            {
                port = this.setPortcomboBox.SelectedItem.ToString();
            }
            catch (Exception exception)
            {
                ok = false;
                saveConfErrorLabel.Text = "*Choose Port";
            }

            try
            {
                baudrate = int.Parse(this.setBaudratecomboBox.SelectedItem.ToString());
            }catch(Exception exception)
            {
                saveConfErrorLabel.Text = "*Choose Baudrate";
                ok = false;
            }
            // TODO: change MCU configurations
            if (ok)
            {
                mSerialPort.PortName = port;
                mSerialPort.BaudRate = baudrate;
                this.Close();
            }

        }
    }
}
