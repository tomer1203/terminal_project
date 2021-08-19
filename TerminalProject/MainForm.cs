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
using System.Runtime.InteropServices;

namespace TerminalProject
{
    public partial class MainForm : Form
    {

        private static CustomSerialPort serialPort;
        private ValueTuple<string, string, int> inData = new ValueTuple<string, string, int>();

        // For Files
        private string currentDirectoryPath;
        private string filesToSendDirectory = "Terminal Project Files/Files to send";
        private string filesRecievedDirectory = "Terminal Project Files/Files recieved";
        private string selectedFilePath = "";
        private string dataFilesPath = "";

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

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

            // Get Current Directory
            currentDirectoryPath = Environment.CurrentDirectory;

            // Serial Port
            serialPort = new CustomSerialPort();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataRecievedHandler);

            try
            {
                serialPort.Open();
                setConnectingLabel(CustomSerialPort.STATUS.PORT_OK);
            }
            catch (Exception) { setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR); }

            EventHub.saveConfigurationsHandler += onSerialConfigurationsChanged;
            EventHub.fileSendingProgressHandler += onFileSendingProgress;

            // List View Initialization
            initializeFilesListView();

        } // END MainForm

        private void Form1_Load(object sender, EventArgs e) { AllocConsole(); }


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
            catch (Exception)
            {
                serialPort.Close();
                updateChatStatusLabel(CustomSerialPort.STATUS.ToString(CustomSerialPort.STATUS.PORT_ERROR));
                this.Invoke((MethodInvoker)delegate
                {
                    setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR);
                });


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
                updateChatStatusLabel(CustomSerialPort.STATUS.ToString(CustomSerialPort.STATUS.CHECKSUM_ERROR));
                updateChatDataLabel("");
                return;
            }
            
            // Check opc
            switch (opc)
            {
                // not in use
                case CustomSerialPort.Type.BAUDRATE:
                    try
                    {
                        serialPort.Close();
                        Thread.Sleep(CustomSerialPort.CONFIGURE_DELAY);
                        serialPort.BaudRate = int.Parse(val);
                        serialPort.Open();
                        this.Invoke((MethodInvoker)delegate
                        {
                            setConnectingLabel(CustomSerialPort.STATUS.PORT_OK);
                        });
                        
                    }
                    catch (Exception) { setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR); }
                    break;

                // Text Message Recieved
                case CustomSerialPort.Type.TEXT:
                    selectTab(0);
                    updateChatDataLabel(val);
                    break;

                // Get MCU Serial Port Status
                case CustomSerialPort.Type.STATUS:
                    handleStatusMessage(int.Parse(val));
                    break;

                // Recieving file
                case CustomSerialPort.Type.FILE_START:
                    selectTab(1);
                    if (CustomSerialPort.RFile.Status == CustomSerialPort.STATUS.RECIEVING_OK)
                    {
                        CustomSerialPort.RFile.Status = CustomSerialPort.STATUS.RECIEVING_FILE;
                        Console.WriteLine("======================================================");
                        Console.WriteLine("recieving a file...");
                        updateFileTransferStatusLabel("recieving a file...");

                    }
                    else {
                        CustomSerialPort.RFile.Status = CustomSerialPort.STATUS.RECIEVING_ERROR;
                        CustomSerialPort.RFile.Status = CustomSerialPort.STATUS.RECIEVING_OK;
                        Console.WriteLine("ERROR - trying to send a new file when already recieving a file");
                        updateFileTransferStatusLabel("ERROR - trying to send a new file when already recieving a file");
                    }
                    break;

                // Recieving file's Name
                case CustomSerialPort.Type.FILE_NAME:
                    if (CustomSerialPort.RFile.Status == CustomSerialPort.STATUS.RECIEVING_ERROR)
                        break;
                    CustomSerialPort.RFile.Name = val;
                    Console.WriteLine("recieving \"" + val + "\"");
                    updateFileTransferStatusLabel("recieving \"" + val + "\"");


                    break;

                // Recieving file's Size
                case CustomSerialPort.Type.FILE_SIZE:
                    if (CustomSerialPort.RFile.Status == CustomSerialPort.STATUS.RECIEVING_ERROR)
                        break;
                    CustomSerialPort.RFile.Size = int.Parse(val);
                    Console.WriteLine("file size: " + val);
                    updateFileTransferStatusLabel("file size: " + val);
                    
                    break;

                // Recieving file's Data
                case CustomSerialPort.Type.FILE_DATA:
                    if (CustomSerialPort.RFile.Status == CustomSerialPort.STATUS.RECIEVING_ERROR)
                    {
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("writing file data ERROR");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        break;
                    }
                    CustomSerialPort.RFile.Data += val;                       
                    Console.WriteLine(val);
                    updateFileTransferStatusLabel("received " + CustomSerialPort.RFile.Data.Length + " / " + CustomSerialPort.RFile.Size + " bytes");

                    if(CustomSerialPort.RFile.Data.Length == CustomSerialPort.RFile.Size)
                    {
                        string path = Path.Combine(currentDirectoryPath, filesRecievedDirectory, CustomSerialPort.RFile.Name);
                        File.WriteAllText(path, CustomSerialPort.RFile.Data);
                        Console.WriteLine("\"" + CustomSerialPort.RFile.Name + "\" received successfully");
                        Console.WriteLine("======================================================");
                        updateFileTransferStatusLabel("\"" + CustomSerialPort.RFile.Name + "\" received successfully");
                        CustomSerialPort.RFile.clean();
                    }
                    break;

                // File recieved ok 
                case CustomSerialPort.Type.FILE_END:
                    if (int.Parse(val) == CustomSerialPort.STATUS.OK)
                    {
                        updateFileTransferStatusLabel("\"" + Path.GetFileName(selectedFilePath) + "\" sent successfully");
                        Console.WriteLine("\"" + Path.GetFileName(selectedFilePath) + "\" sent successfully");
                        Console.WriteLine("======================================================");
                    }
                    else
                    {
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("\"" + CustomSerialPort.RFile.Name + "\" did not send successfully");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        updateFileTransferStatusLabel("\"" + CustomSerialPort.RFile.Name + "\" did not send successfully");
                    }
                    break;

                // Unknown
                default:
                    Console.WriteLine("unreccognized type");
                    updateFileTransferStatusLabel("unreccognized type");
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
                case CustomSerialPort.STATUS.RECIEVING_MESSAGE:
                    updateChatStatusLabel(CustomSerialPort.STATUS.ToString(status));
                    break;

                case CustomSerialPort.STATUS.BUFFER_ERROR:
                    updateChatStatusLabel(CustomSerialPort.STATUS.ToString(status));
                    CustomSerialPort.STATUS.bufferErrorCounter++;
                    if(CustomSerialPort.STATUS.bufferErrorCounter > CustomSerialPort.STATUS.BUFFER_ERROR_OVERFLOW)
                    {
                        CustomSerialPort.STATUS.bufferErrorCounter = 0;
                        this.Invoke((MethodInvoker)delegate
                        {
                            setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR);
                        });
                    }
                    break;

                case CustomSerialPort.STATUS.OK:
                    updateChatStatusLabel(CustomSerialPort.STATUS.ToString(status));
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
            Brush brush = Brushes.Green;
            switch (status)
            {
                case CustomSerialPort.STATUS.PORT_OK:
                case CustomSerialPort.STATUS.OK:
                    brush = Brushes.Green;
                    this.connectingLabel.Text = "Connected to " + serialPort.PortName + " with Baudrate " + serialPort.BaudRate + " BPS";
                    break;

                case CustomSerialPort.STATUS.PORT_ERROR:
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
            this.Invoke((MethodInvoker)delegate
            {
                setConnectingLabel(CustomSerialPort.STATUS.OK);
            });
        }

        /*
         * File Sending Progress updater
         */ 
        private void onFileSendingProgress(object sender, EventArgs e)
        {
            updateFileTransferStatusLabel(sender.ToString());
        }

        /*
         * Select Tab
         */
        private void selectTab(int index)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    tabControl1.SelectTab(index);
                });
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                Chat Tab
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /*
         * Update Chat Status labels
         */
        private void updateChatStatusLabel(string statusLabelString)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    statusLabel.Text = statusLabelString;
                });
            }
            else
                statusLabel.Text = statusLabelString;
        }

        /*
        * Update Chat Data Recieved Label
        */
        private void updateChatDataLabel(string dataRecievedLabelString)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    dataRecieveLabel.Text = dataRecievedLabelString;
                });
            }
            else
                dataRecieveLabel.Text = dataRecievedLabelString;
           
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
                setConnectingLabel(CustomSerialPort.STATUS.PORT_OK);
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
        public void updateFileTransferStatusLabel(string fileStatusLabelString)
        {
            this.Invoke((MethodInvoker)delegate
            {
                fileStatusLabel.Text = fileStatusLabelString;
            });
        }

        public void outsideUpdateFileTransferUI(string fileStatusLabelString)
        {
            fileStatusLabel.Invoke(new MethodInvoker(() => fileStatusLabel.Text = fileStatusLabelString));
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
                CustomSerialPort.RFile.clean();
                fileStatusLabel.Text = "sending \"" + Path.GetFileName(selectedFilePath) + "\"";
                Console.WriteLine("========================================================");
                Console.WriteLine("sending \"" + Path.GetFileName(selectedFilePath) + "\"");
                serialPort.sendFile(selectedFilePath);
            }
            catch (Exception exception) {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("error sending \"" + Path.GetFileName(selectedFilePath) + "\"" + exception.ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                fileStatusLabel.Text = "error sending \"" + Path.GetFileName(selectedFilePath) + "\"";
                setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR);
            }
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
