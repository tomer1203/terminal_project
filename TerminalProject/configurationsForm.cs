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
            this.mSerialPort = serialPort;
            InitializeComponent();

            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            
            // Set list of available ports
            this.setPortcomboBox.Items.AddRange(SerialPort.GetPortNames());
            // Set list of available baudrates
            this.setBaudratecomboBox.Items.AddRange(new object[] {
            "2400",
            "9600",
            "19200",
            "38400"});

            
        }

        /*
         * Save Configuration
         */ 
        private void saveConfButton_Click(object sender, EventArgs e)
        {
            bool status_ok = true;
            String port = "COM1";
            int baudrate = 9600;
            try
            {
                port = this.setPortcomboBox.SelectedItem.ToString();
            }
            catch (Exception)
            {
                status_ok = false;
                saveConfErrorLabel.Text = "*Choose Port";
            }

            try
            {
                // get baudrate as integer
                baudrate = int.Parse(this.setBaudratecomboBox.SelectedItem.ToString());
            }catch(Exception)
            {
                saveConfErrorLabel.Text = "*Choose Baudrate";
                status_ok = false;
            }
            
            if (status_ok)
            {
                string data = "";
                // close opened serial port
                if (mSerialPort.IsOpen){
                    mSerialPort.Close();
                }
                mSerialPort.PortName = port;
                try
                {
                    mSerialPort.Open();
                    data = "$[Br]" + baudrate.ToString() + '\0';
                    mSerialPort.Write(data);
                    mSerialPort.Close();
                }
                catch (Exception) { }
                    
                mSerialPort.BaudRate = baudrate;
                try
                {
                    mSerialPort.Open();
                    this.Close();
                }
                catch (Exception exception) {
                    saveConfErrorLabel.Text = exception.ToString();
                };
                
            }

        }
    }
}
