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

        /*
         * Construstor
         */ 
        public MainForm()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            serialPort = new SerialPort();
            string[] ports = SerialPort.GetPortNames().Length > 0 ? SerialPort.GetPortNames() : new string[] { "No Available Ports" };
            serialPort.PortName = ports[0]; // default
            serialPort.BaudRate = 9600; // default
            serialPort.ReadTimeout = 300;
            serialPort.WriteTimeout = 300;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataRecievedHandler);

            try
            {
                serialPort.Open();
                setConnectingLabel(SerialPortStatus.STATUS_OK);
            }
            catch (Exception) { setConnectingLabel(SerialPortStatus.STATUS_ERROR); }

            // Get a list of serial port names.
            dataRecieveRichTextBox.Text = string.Join(", ", ports);

            Source_files.EventHub.saveConfigurationsHandler += onConfighrationsChanged;

        }

        /*
         * Listens to UART interrupts
         */ 
        private void DataRecievedHandler(Object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = "";
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            try {
                inData = sp.ReadLine();
            } catch (Exception)
            {
                serialPort.Close();
              //  setConnectingLabel(SerialPortStatus.STATUS_ERROR);
            }
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            stringRecieveRichTextBox.Invoke((MethodInvoker)delegate
            {
                stringRecieveRichTextBox.Text = inData;
            });
            }

       

        /*
         * Send Data to MCU 
         */
        private void sendButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            try
            {
                serialPort.Write(dataToSendTextBox.Text + '\0');
                dataToSendTextBox.Text = "";
            }
            catch (Exception)
            {
                setConnectingLabel(SerialPortStatus.STATUS_ERROR);
            } 
            
        }

        /*
         * Set Configuretions
         */ 
       private void configurationButton_click(object sender, EventArgs e)
        {
            // start configuration window
            if ((Application.OpenForms["ConfigurationsForm"] as ConfigurationsForm) != null)
            {
                //Form is already open
                Application.OpenForms["ConfigurationsForm"].Close();
            }
           
            ConfigurationsForm configurationsForm = new ConfigurationsForm(ref serialPort);
            configurationsForm.MinimizeBox = false;
            configurationsForm.MaximizeBox = false;
            configurationsForm.Show();

            
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

        /*
         * Settings Button Mouse Hover
         */ 
        private void settingsPanel_MouseHover(Object sender, EventArgs e)
        {
            this.settingsPanel.BackColor = Color.LightSeaGreen;
        }

        /*
         * Settings Button Mouse Leave
         */
        private void settingsPanel_MouseLeave(Object sender, EventArgs e)
        {
            this.settingsPanel.BackColor = Color.Transparent;
        }

        private void Form1_Load(object sender, EventArgs e){}


        /*
         * Confighrations Change Listener
         */
        private void onConfighrationsChanged(object sender, EventArgs e)
        {
            setConnectingLabel(SerialPortStatus.STATUS_OK);
        }

        /*
         * Dispaly connectingt label
         */ 
        private void setConnectingLabel(int status)
        {

            Brush brush = Brushes.Red;
            switch (status)
            {
                case SerialPortStatus.STATUS_OK:
                    brush = Brushes.Green;
                    this.connectingLabel.Text = "Connected to " + serialPort.PortName + " with Baudrate " + serialPort.BaudRate + " BPS";
                    break;

                case SerialPortStatus.STATUS_ERROR:
                    brush = Brushes.Red;
                    this.connectingLabel.Text = "Configure Serial Port";
                    break;

            }
            int nSize = 8;
            int x = (panel1.Size.Width - connectingLabel.Size.Width) / 2;
            int y = (panel1.Size.Height - connectingLabel.Size.Height) / 2; ;
            connectingLabel.Location = new Point(x, y);
            connectingPictureBox.Location = new Point(connectingLabel.Location.X - 13, connectingLabel.Location.Y + 3);
            Bitmap bm = new Bitmap(connectingPictureBox.Width, connectingPictureBox.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gr.FillEllipse(brush, Convert.ToInt32((connectingPictureBox.Width - nSize) / 2), Convert.ToInt32((connectingPictureBox.Height - nSize) / 2), nSize, nSize);
            }
           
            connectingPictureBox.Image = bm;

        }

    }

    /*
     * Class of constants to hold Serial Port status
     */
    static class SerialPortStatus
    {
        public const int STATUS_OK = 0, STATUS_ERROR = 1; 

    } 


}
