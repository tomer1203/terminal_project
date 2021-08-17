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
        private string filesToSendDirectory = "Terminal Project Files/Files to send";
        private string selectedFilePath = "";
        private string dataFilesPath = "";

        /*
         * Construstor
         */
        public MainForm()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            tabControl1.Width = this.Width;

            this.dataRecieveLabel.Parent = this.dataRecievePanel;
            this.dataRecieveLabel.Text = "";

            // Serial Port
            serialPort = new CustomSerialPort();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataRecievedHandler);

            try
            {
                serialPort.Open();
                setConnectingLabel(CustomSerialPort.STATUS.OK);
            }
            catch (Exception) { setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR); }

            EventHub.saveConfigurationsHandler += onSerialConfigurationsChanged;

            // List View Initialization
            initializeFilesListView();

        } // END MainForm

        private void Form1_Load(object sender, EventArgs e) { }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////           Main Methods
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
                updateChatUI("", exception.ToString(), CustomSerialPort.STATUS.PORT_ERROR);
                
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
            if (checksumStatus == CustomSerialPort.STATUS.CHECKSUM_ERROR)
            {
                updateChatUI("STATUS_CHECKSUM_ERROR", serialPort.lastMessage, -1);
                return;
            }

            // Check opc
            switch (opc)
            {
                // not in use
                case CustomSerialPort.Type.BAUDRATE:
                    break;

                // Text Message Recieved
                case CustomSerialPort.Type.TEXT:
                    updateChatUI( "TEXT_RECIEVED", val, -1);    
                    break;

                // Get MCU Serial Port Status
                case CustomSerialPort.Type.STATUS:
                    handleStatusMessage(int.Parse(val));
                    break;

                // File recieved ok 
                case CustomSerialPort.Type.FILE_END:
                    if(int.Parse(val) == CustomSerialPort.STATUS.OK)
                        updateFileTransferUI("File Sent Successfully");
                    else
                        updateFileTransferUI("File didnot Sent Successfully");
                    break;

                // Recieving file
                case CustomSerialPort.Type.FILE_START:
                    updateFileTransferUI("Recieving file...");
                    break;

                // Recieving file's Name
                case CustomSerialPort.Type.FILE_NAME:
                    updateFileTransferUI("Recieving file name: " + val);
                    break;

                // Recieving file's Size
                case CustomSerialPort.Type.FILE_SIZE:
                    updateFileTransferUI("Receiving file size: " + val);
                    break;

                // Recieving file's Data
                case CustomSerialPort.Type.FILE_DATA:
                   // TODO: something
                    break;


                // Unknown
                default:
                    updateFileTransferUI("Unreccognized type");
                    break;


            }
       
        } // END handleMessage

        /*
         * Handle Status Type Message
         */ 
        private void handleStatusMessage(int status)
        {
            switch (status)
            {
                case CustomSerialPort.STATUS.RECIEVING:
                    updateChatUI(CustomSerialPort.STATUS.ToString(status), "Fetching Data...", status);
                    break;

                case CustomSerialPort.STATUS.BUFFER_ERROR:
                    updateChatUI(CustomSerialPort.STATUS.ToString(status), "Buffer Error. Send Again", status);
                    break;

                case CustomSerialPort.STATUS.OK:
                    updateChatUI(CustomSerialPort.STATUS.ToString(status), "", status);
                    break;
                default:
                    // TODO: maybe UI change
                    break;
            }

        }

        /*
        * Dispaly connectingt label
        */
        private void setConnectingLabel(int status)
        {
            Brush brush = Brushes.Red;
            switch (status)
            {
                case CustomSerialPort.STATUS.OK:
                    brush = Brushes.Green;
                    this.connectingLabel.Text = "Connected to " + serialPort.PortName + " with Baudrate " + serialPort.BaudRate + " BPS";
                    break;

                case CustomSerialPort.STATUS.PORT_ERROR:
                    brush = Brushes.Red;
                    this.connectingLabel.Text = "Configure Serial Port";
                    break;

                default:
                    brush = Brushes.Orange;
                    this.connectingLabel.Text = "Unrecognized Status: " + CustomSerialPort.STATUS.ToString(status);
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
        * Serial Button Mouse Hover
        */
        private void settingsPanel_MouseHover(Object sender, EventArgs e)
        {
            this.settingsPanel.BackColor = Color.LightSeaGreen;
        }

        /*
         * Serial Button Mouse Leave
         */
        private void settingsPanel_MouseLeave(Object sender, EventArgs e)
        {
            this.settingsPanel.BackColor = Color.Transparent;
        }

       /*
        * Serial Confighrations Change Listener
        */
        private void onSerialConfigurationsChanged(object sender, EventArgs e)
        {
            setConnectingLabel(CustomSerialPort.STATUS.OK);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                Chat Tab
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /*
         * Update Chat UI labels
         */
        private void updateChatUI(string statusLabelString, string dataRecievedLabelString, int setConnectionLabelWithStatus)
        {
            this.Invoke((MethodInvoker)delegate
            {
                // update Status Label
                if (!statusLabel.Equals(""))
                { 
                    statusLabel.Text = statusLabelString;
                    Console.Write(statusLabelString);
                }

                // update Data Recieved Label 
                if(!dataRecievedLabelString.Equals(""))
                    dataRecieveLabel.Text = dataRecievedLabelString;

                // update Connecting Label
                if (setConnectionLabelWithStatus != -1)
                    setConnectingLabel(setConnectionLabelWithStatus);

            });
        }


        /*
         * On Send Message Button Click 
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
                setConnectingLabel(CustomSerialPort.STATUS.CHECKSUM_ERROR);
            }
        }


        /*
         * NFB Button click to send data using Write method
         */ 
        private void nonFormatButon_Click(object sender, EventArgs e)
        {
            serialPort.Write(dataToSendTextBox.Text);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //          Files Tab
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

       /*
        * Updates File Transfer UI Labels
        */
        private void updateFileTransferUI(string fileStatusLabelString)
        {
            this.Invoke((MethodInvoker)delegate
            {
                // update Status Label
                if (!fileStatusLabelString.Equals(""))
                {
                    fileStatusLabel.Text = fileStatusLabelString;
                    Console.Write(fileStatusLabelString);
                }

            });
        }

       /*
        * On List View Item Click
        */
        private void filesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filesListView.SelectedItems.Count >= 1)
            {
                string selectedFile = filesListView.SelectedItems[0].Text;
                string[] fileEntries = Directory.GetFiles(dataFilesPath);
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
            // get Directory
            string currentDirectoryPath = Environment.CurrentDirectory;
            dataFilesPath = Path.Combine(currentDirectoryPath, filesToSendDirectory);

            if (Directory.Exists(dataFilesPath))
            {
                // Process the list of files found in the directory
                ListViewItem listViewItem;
                string[] fileEntries = Directory.GetFiles(dataFilesPath);
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
                fileStatusLabel.Text = "Sending " + Path.GetFileName(selectedFilePath);
                serialPort.sendFile(selectedFilePath);
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
