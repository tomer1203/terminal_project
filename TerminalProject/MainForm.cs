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
using System.IO;
using System.Threading;

namespace TerminalProject
{
    public partial class MainForm : Form
    {

        private static CustomSerialPort serialPort;
        private ValueTuple<string, string, int> inData = new ValueTuple<string, string, int>();

        // For Files
        private string filesToSendPath = "C:/Users/טל/Desktop/Terminal Project Files/Files to send";
        private string selectedFilePath = "";

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

            initializeFilesListView();

        } // END MainForm

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////            Methods
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*
         * Listens to UART interrupts
         */
        private void DataRecievedHandler(Object sender, SerialDataReceivedEventArgs e)
        {
            CustomSerialPort sp = (CustomSerialPort)sender;
            try
            {
                inData = sp.readMessage();

            }
            catch (Exception exception)
            {
                serialPort.Close();
                updateUI("", exception.ToString(), CustomSerialPort.STATUS_CHECKSUM_ERROR);
                
            }
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            handleMessage(inData.Item1, inData.Item2, inData.Item3);

        }


        /// <summary>
        /// Handle Message
        /// </summary>
        /// <param name="opc">operation code</param>
        /// <param name="val">value</param>
        /// <param name="checksumStatus">check sum</param>
        private void handleMessage(string opc, string val, int checksumStatus)
        {
            // Checksum check
            if (checksumStatus == CustomSerialPort.STATUS_CHECKSUM_ERROR)
            {
                updateUI("STATUS_CHECKSUM_ERROR", serialPort.lastMessage, -1);
                return;
            }


            switch (opc)
            {
                // get Baudrate
                case CustomSerialPort.Type.BAUDRATE:
                    break;

                case CustomSerialPort.Type.TEXT:
                    updateUI( "TEXT_RECIEVED", val, -1);    
                    break;

                // get MCU Serial Port Status
                case CustomSerialPort.Type.STATUS:
                    if (int.Parse(val) == CustomSerialPort.STATUS_RECIEVING)
                    {
                        updateUI("STATUS_RECIEVING", "Fetching Data...", -1);
                        break;
                    }
                    else if (int.Parse(val) == CustomSerialPort.STATUS_BUFFER_ERROR)
                    {
                        updateUI("STATUS_BUFFER_ERROR", "Buffer Error. Send Again", -1);
                        break;
                    }
                    else if (int.Parse(val) == CustomSerialPort.STATUS_OK)
                    {
                        updateUI("STATUS_OK", "", -1);
                    }
                    
                    updateUI("", "", int.Parse(val));
                    break;
            }
       
        } // END handleMessage

        /*
         * Update UI labels
         */ 
        private void updateUI(string statusLabelString, string dataRecievedLabelString, int setConnectionLabelWithStatus)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (!statusLabel.Equals(""))
                { // update Status Label
                    statusLabel.Text = statusLabelString;
                    Console.Write(statusLabelString);
                }

                // update Data Recieved Label 
                if(!dataRecievedLabelString.Equals(""))
                    dataRecieveLabel.Text = dataRecievedLabelString;

                if (setConnectionLabelWithStatus != -1)
                    setConnectingLabel(setConnectionLabelWithStatus);

            });
        }


        /*
         * On Send Button Click 
         */
        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            if (dataToSendTextBox.Text.Equals(""))
                return;

            // Send Message to MCU
            try
            {   
                serialPort.sendMessage(CustomSerialPort.Type.TEXT, dataToSendTextBox.Text);
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
        private void mainForm_KeyDown(Object sender, KeyEventArgs keyEvent)
        {
            if (keyEvent.KeyCode == Keys.Enter)
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
                    sendMessageButton_Click(sender, new EventArgs());
                else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
                    sendFileButton_Click(sender, new EventArgs());
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

        private void Form1_Load(object sender, EventArgs e) { }


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

        /*
         * NFB Button click to send data using Write method
         */ 
        private void nonFormatButon_Click(object sender, EventArgs e)
        {
            serialPort.Write(dataToSendTextBox.Text);
        }

        /*
         * On List View Item Click
         */
        private void filesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filesListView.SelectedItems.Count >= 1)
            {
                string selectedFile = filesListView.SelectedItems[0].Text;
                string[] fileEntries = Directory.GetFiles(filesToSendPath);
                foreach (string filePath in fileEntries)
                {
                    if (Path.GetFileName(filePath).Equals(selectedFile) && Path.GetExtension(filePath) == ".txt")
                    {
                        selectedFilePath = filePath;
                    }
                }

            }
        }

        /*
         * Initialize Files Text View with files 
         */
        private void initializeFilesListView()
        {
            this.filesListView.Scrollable = true;
            // Set Column
            this.filesListView.Columns.Add("Name", 600, HorizontalAlignment.Left);
            this.filesListView.Columns.Add("Size", -2, HorizontalAlignment.Left);

            if (Directory.Exists(filesToSendPath))
            {
                string currentDirectoryPath = Environment.CurrentDirectory;
                //string dataFilesPath = Path.Combine(currentDirectoryPath, );
                // Process the list of files found in the directory.
                ListViewItem listViewItem;
                string[] fileEntries = Directory.GetFiles(filesToSendPath);
                foreach (string filePath in fileEntries)
                {
                    FileInfo file = new FileInfo(filePath);
                    if (file.Extension.Equals(".txt"))
                    {
                        string[] row = { file.Name, file.Length.ToString() + " Bytes" };
                        listViewItem = new ListViewItem(row);
                        filesListView.Items.Add(listViewItem);
                    }
                }
            }
           
            filesListView.Focus();
        }

        /*
         * Send File Button Click
         */ 
        private void sendFileButton_Click(object sender, EventArgs e)
        {
            if (selectedFilePath.Equals(""))
                return;

            // Send File to MCU
            try
            {   
                serialPort.sendFile(selectedFilePath);
                fileStatusLabel.Text = "Sending "  + Path.GetFileName(selectedFilePath);
            }
            catch (Exception) { }
        }

        /*
         * Prevent column resize
         */ 
        private void filesListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = filesListView.Columns[e.ColumnIndex].Width;
        }
    }
}
