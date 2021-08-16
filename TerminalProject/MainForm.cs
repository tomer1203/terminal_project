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
using System.IO.Ports;
using System.Threading;

namespace TerminalProject
{
    public partial class MainForm : Form
    {

        static CustomSerialPort serialPort;
        private ValueTuple<string, string, int> inData = new ValueTuple<string, string, int>();

        /*
         * Construstor
         */ 
        public MainForm()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.dataRecieveLabel.Parent = this.dataRecievePanel;
            this.dataRecieveLabel.Text = "";

            serialPort = new CustomSerialPort();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataRecievedHandler);

            try
            {
                serialPort.Open();
                setConnectingLabel(CustomSerialPort.STATUS_OK);
            }
            catch (Exception) { setConnectingLabel(CustomSerialPort.STATUS_CHECKSUM_ERROR); }

            EventHub.saveConfigurationsHandler += onConfighrationsChanged;

        }

        /*
         * Listens to UART interrupts
         */ 
        private void DataRecievedHandler(Object sender, SerialDataReceivedEventArgs e)
        {
            CustomSerialPort sp = (CustomSerialPort)sender;
            try {
                 inData = sp.readMessage();
                handleMessage(inData.Item1, inData.Item2, inData.Item3);
            } catch (Exception)
            {
                serialPort.Close();
                this.Invoke((MethodInvoker)delegate {
                    setConnectingLabel(CustomSerialPort.STATUS_CHECKSUM_ERROR);
                }); 
            }

        }

        private void handleMessage(string opc, string val, int checksumStatus)
        {
            if(checksumStatus == CustomSerialPort.STATUS_CHECKSUM_ERROR)
            {
                // update UI
                dataRecieveLabel.Invoke((MethodInvoker)delegate
                {
                    dataRecieveLabel.Text = "STATUS_CHECKSUM_ERROR" + serialPort.lastMessage;
                });
                return;
            }

            switch (opc)
            {
                case CustomSerialPort.BAUDRATE: // get Baudrate
                    break;

                case CustomSerialPort.STATUS: // get MCU Serial Port Status
                    if(int.Parse(val) == CustomSerialPort.STATUS_RECIEVING)
                    {
                        // update UI
                        dataRecieveLabel.Invoke((MethodInvoker)delegate
                        {
                            dataRecieveLabel.Text = "Fetching Data...";
                        });
                        break;
                    }
                    // update UI connecting label
                    this.Invoke((MethodInvoker)delegate {
                        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
                        setConnectingLabel(int.Parse(val));
                    });
                    break;

                case CustomSerialPort.TEXT:
                    // update UI
                    dataRecieveLabel.Invoke((MethodInvoker)delegate
                    {
                        dataRecieveLabel.Text = val;
                    });
                    break;
            }

        }


        /*
         * On Send Button Click 
         */
        private void sendButton_Click(object sender, EventArgs e)
        {
            sendStringToSerialPost();
        }

        /*
         * Send Data to MCU 
         */
        private void sendStringToSerialPost()
        {
            try
            {
                serialPort.sendMessage(CustomSerialPort.TEXT, dataToSendTextBox.Text);
                dataToSendTextBox.Text = "";
            }
            catch (Exception)
            {
                setConnectingLabel(CustomSerialPort.STATUS_CHECKSUM_ERROR);
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
            setConnectingLabel(CustomSerialPort.STATUS_OK);
        }

        /*
         * Dispaly connectingt label
         */ 
        private void setConnectingLabel(int status)
        {

            Brush brush = Brushes.Red;
            switch (status)
            {
                case CustomSerialPort.STATUS_OK:
                    brush = Brushes.Green;
                    this.connectingLabel.Text = "Connected to " + serialPort.PortName + " with Baudrate " + serialPort.BaudRate + " BPS";
                    break;

                case CustomSerialPort.STATUS_CHECKSUM_ERROR:
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

}
