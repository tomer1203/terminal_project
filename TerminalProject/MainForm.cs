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

            serialPort = new SerialPort();
            serialPort.PortName = "COM6";
            serialPort.ReadTimeout = 3000;
            serialPort.WriteTimeout = 300;
            serialPort.BaudRate = 9600;
            serialPort.Open();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataRecievedHandler);
            _continue = true;

            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();
            dataRecieveRichTextBox.Text = string.Join(", ", ports);

        }

        private void DataRecievedHandler(Object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = "";
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            inData = sp.ReadLine();
            serialPort.ReadTimeout = 3000;
            /*try
            {
                inData = sp.ReadLine();
            }catch (Exception ignore) { };*/
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            dataRecieveRichTextBox.Invoke((MethodInvoker)delegate
            {
                dataRecieveRichTextBox.Text = inData;
            });
        }

        /*
         * Reads Data from MCU 
         */
        private void serialButton_Click(object sender, EventArgs e)
        {

            //serialPort.BaudRate = SetPortBaudRate(serialPort.BaudRate);
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
            serialPort.Write(dataToSendTextBox.Text + '\n');
            dataToSendTextBox.Text = "";

            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Green);

            SolidBrush sb = new SolidBrush(Color.Green);
            g.DrawEllipse(p, connectingLabel.Location.X - 10, connectingLabel.Location.Y + 4, 5, 5);
            g.FillEllipse(sb, connectingLabel.Location.X - 10, connectingLabel.Location.Y + 4, 5, 5);

            this.connectingLabel.Text = serialPort.IsOpen.ToString();

        }

        /*
         * Set Configuretions
         */ 
       private void configurationButton_click(object sender, EventArgs e)
        {
            ConfigurationsForm configurationsForm = new ConfigurationsForm(ref serialPort);
            configurationsForm.MinimizeBox = false;
            configurationsForm.MaximizeBox = false;
            configurationsForm.Show();
        }

        /*
         * Listen to EnterKey
         */ 
        private void dataToSendTextBox_KeyDown(Object sender, KeyEventArgs keyEvent)
        {
            if(keyEvent.KeyCode == Keys.Enter)
            {
                sendButton_Click(sender, new EventArgs());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        /*
         * Display BaudRate values and prompt user to enter a value.
         */
        private int SetPortBaudRate(int defaultPortBaudRate)
        {
            string baudRate;

            this.enterStringLabel.Text = "Enter Baudrate:";
            baudRate = this.dataToSendTextBox.Text;

            if (baudRate == "")
            {
                baudRate = defaultPortBaudRate.ToString();
            }

            return int.Parse(baudRate);
        }

        
    }
}
