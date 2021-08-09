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
using System.Threading;

namespace TerminalProject
{
    public partial class MainForm : Form
    {

        static SerialPort serialPort;
        static bool _continue;

        public MainForm()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            serialPort = new SerialPort();
            string[] ports = SerialPort.GetPortNames().Length > 0 ? SerialPort.GetPortNames() : new string[] { "" };
            serialPort.PortName = "COM1";//ports[0]; // default
            serialPort.BaudRate = 9600; // default
            serialPort.ReadTimeout = 300;
            serialPort.WriteTimeout = 300;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataRecievedHandler);
            _continue = true;

            try
            {
                serialPort.Open();
                setConnectingLabel(SerialPortStatus.STATUS_OK);
            }
            catch (Exception) { setConnectingLabel(SerialPortStatus.STATUS_ERROR); }

            // Get a list of serial port names.
            dataRecieveRichTextBox.Text = string.Join(", ", ports);

        }

        private void DataRecievedHandler(Object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = "";
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            inData = sp.ReadLine();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            stringRecieveRichTextBox.Invoke((MethodInvoker)delegate
            {
                stringRecieveRichTextBox.Text = inData;
            });
        }

        /*
         * Reads Data from MCU 
         */
        private void serialButton_Click(object sender, EventArgs e)
        {
            try
            {
                dataRecieveRichTextBox.Text = serialPort.ReadLine();
                serialPort.WriteLine("Got it");
            }
            catch (TimeoutException) { }
            
        }

        /*
         * Send Data to MCU 
         */
        private void sendButton_Click(object sender, EventArgs e)
        {
            serialPort.Write(dataToSendTextBox.Text + '\0');
            dataToSendTextBox.Text = "";
        }

        /*
         * Set Configuretions
         */ 
       private void configurationButton_click(object sender, EventArgs e)
        {
            // start configuration window
            ConfigurationsForm configurationsForm = new ConfigurationsForm(ref serialPort);
            configurationsForm.MinimizeBox = false;
            configurationsForm.MaximizeBox = false;
            configurationsForm.Show();

            setConnectingLabel(SerialPortStatus.STATUS_OK);

        }

        /*
         * Listen to EnterKey to send data
         */ 
        private void dataToSendTextBox_KeyDown(Object sender, KeyEventArgs keyEvent)
        {
            if(keyEvent.KeyCode == Keys.Enter)
            {
                sendButton_Click(sender, new EventArgs());
            }
        }

        private void Form1_Load(object sender, EventArgs e){}

        private void setConnectingLabel(int status)
        {
            switch(status)
            {
                case SerialPortStatus.STATUS_OK:
                    this.connectingLabel.Text = "Connected to " + serialPort.PortName + " with baudrate " + serialPort.BaudRate;
                    Graphics g = this.CreateGraphics();
                    Pen p = new Pen(Color.Green);
                    SolidBrush sb = new SolidBrush(Color.Green);
                    g.DrawEllipse(p, connectingLabel.Location.X-5, connectingLabel.Location.Y-5, 5, 5);
                    g.FillEllipse(sb, connectingLabel.Location.X-5, connectingLabel.Location.Y-5, 5, 5);
                    
                    break;
                case SerialPortStatus.STATUS_ERROR:
                    this.connectingLabel.Text = "Configure Serial Port";
                    break;
            }

            
        }


    }

    static class SerialPortStatus
    {
        public const int STATUS_OK = 0, STATUS_ERROR = 1; 

    } 
}
