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
using TerminalProject.Source_files;
using System.Threading;

namespace TerminalProject
{
    public partial class ConfigurationsForm : Form
    {
        private CustomSerialPort mSerialPort;

        /*
         * Construstor
         */
        public ConfigurationsForm(ref CustomSerialPort serialPort)
        {
            
            InitializeComponent();

            this.mSerialPort = serialPort;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            // Set list of available ports
            string[] ports = SerialPort.GetPortNames().Length > 0 ? SerialPort.GetPortNames() : null;
            if (ports != null)
            { 
                this.setPortcomboBox.Items.AddRange(ports);
                // enable save button
                saveConfButton.Click += new System.EventHandler(this.saveConfButton_Click);
                this.setPortcomboBox.SelectedItem = serialPort.PortName;
            }
            else
            {   // no available ports
                saveConfErrorLabel.Text = "no available ports";
                // disable save button
                saveConfButton.Click -= this.saveConfButton_Click;

            }
            // Set list of available baudrates
            this.setBaudratecomboBox.Items.AddRange(new object[] {
            "2400",
            "9600",
            "19200",
            "38400"});
            this.setBaudratecomboBox.SelectedItem = serialPort.BaudRate.ToString();
        }

        /*
         * Save Configuration
         */ 
        private void saveConfButton_Click(object sender, EventArgs e)
        {
            bool status_ok = true;
            string[] availablePorts = SerialPort.GetPortNames().Length > 0 ? SerialPort.GetPortNames() : new string[] { "No Available Ports" };
            String port = availablePorts[0];
            int baudrate = 9600; // default value
            try
            {  // get port name  
                port = this.setPortcomboBox.SelectedItem.ToString();
            }
            catch (Exception)
            {
                status_ok = false;
                saveConfErrorLabel.Text = "* Choose Port";
            }

            try
            {
                // get baudrate as integer
                baudrate = int.Parse(this.setBaudratecomboBox.SelectedItem.ToString());
            }catch(Exception)
            {
                saveConfErrorLabel.Text = "* Choose Baudrate";
                status_ok = false;
            }
            
            if (status_ok) // user chose both baudrate and port
            {
                string data = "";
                // close opened serial port
                if (mSerialPort.IsOpen){
                    mSerialPort.Close();
                    // Delay is needed befor opening the port again
                    Thread.Sleep(50);
                }
                mSerialPort.PortName = port;
                try
                {
                    mSerialPort.Open();
                    mSerialPort.sendMessage(CustomSerialPort.TYPE_BAUDRATE, baudrate.ToString());
                    mSerialPort.Close();
                    // Delay is needed befor opening the port again
                    Thread.Sleep(50);
                }
                catch (Exception) { }
                mSerialPort.clearMyBuffer();
                mSerialPort.BaudRate = baudrate;
                try
                {
                    mSerialPort.Open();
                    EventHub.OnSaveConfigurations(mSerialPort, EventArgs.Empty);
                    // Close window
                    this.Close();
                }
                catch (Exception)
                {
                    saveConfErrorLabel.Text = "port already open";
                };

            }

        }

        /*
         * Listen to EnterKey to send data
         */
        private void saveConfButton_KeyDown(Object sender, KeyEventArgs keyEvent)
        {
            if (keyEvent.KeyCode == Keys.Enter)
            {
                saveConfButton_Click(sender, new EventArgs());
            }
        }
    }
}
