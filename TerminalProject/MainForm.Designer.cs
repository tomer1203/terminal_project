namespace TerminalProject
{
    partial class MainForm
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sendButton = new System.Windows.Forms.Button();
            this.dataToSendTextBox = new System.Windows.Forms.TextBox();
            this.enterStringLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.statusLabel = new System.Windows.Forms.Label();
            this.nonFormatButon = new System.Windows.Forms.Button();
            this.dataRecievePanel = new System.Windows.Forms.Panel();
            this.dataRecieveLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.filesListView = new System.Windows.Forms.ListView();
            this.fileNamePanel = new System.Windows.Forms.Panel();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.connectingLabel = new System.Windows.Forms.Label();
            this.connectingPictureBox = new System.Windows.Forms.PictureBox();
            this.configurationsLabel = new System.Windows.Forms.Label();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.sendFileButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.dataRecievePanel.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.fileNamePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.connectingPictureBox)).BeginInit();
            this.settingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.sendButton.ForeColor = System.Drawing.Color.Teal;
            this.sendButton.Location = new System.Drawing.Point(748, 45);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(61, 36);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // dataToSendTextBox
            // 
            this.dataToSendTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataToSendTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataToSendTextBox.Location = new System.Drawing.Point(39, 45);
            this.dataToSendTextBox.Name = "dataToSendTextBox";
            this.dataToSendTextBox.Size = new System.Drawing.Size(703, 30);
            this.dataToSendTextBox.TabIndex = 0;
            this.dataToSendTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataToSendTextBox_KeyDown);
            // 
            // enterStringLabel
            // 
            this.enterStringLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.enterStringLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.enterStringLabel.Location = new System.Drawing.Point(35, 17);
            this.enterStringLabel.Name = "enterStringLabel";
            this.enterStringLabel.Size = new System.Drawing.Size(200, 25);
            this.enterStringLabel.TabIndex = 0;
            this.enterStringLabel.Text = "Enter String:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 35);
            this.tabControl1.Location = new System.Drawing.Point(-3, 54);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(179, 2);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(857, 500);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.statusLabel);
            this.tabPage1.Controls.Add(this.nonFormatButon);
            this.tabPage1.Controls.Add(this.dataRecievePanel);
            this.tabPage1.Controls.Add(this.sendButton);
            this.tabPage1.Controls.Add(this.enterStringLabel);
            this.tabPage1.Controls.Add(this.dataToSendTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(849, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chat";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(378, 386);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(53, 20);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Text = "status";
            // 
            // nonFormatButon
            // 
            this.nonFormatButon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nonFormatButon.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.nonFormatButon.ForeColor = System.Drawing.Color.Teal;
            this.nonFormatButon.Location = new System.Drawing.Point(748, 6);
            this.nonFormatButon.Name = "nonFormatButon";
            this.nonFormatButon.Size = new System.Drawing.Size(61, 36);
            this.nonFormatButon.TabIndex = 3;
            this.nonFormatButon.Text = "NFB";
            this.nonFormatButon.UseVisualStyleBackColor = true;
            this.nonFormatButon.Click += new System.EventHandler(this.nonFormatButon_Click);
            // 
            // dataRecievePanel
            // 
            this.dataRecievePanel.Controls.Add(this.dataRecieveLabel);
            this.dataRecievePanel.Location = new System.Drawing.Point(35, 98);
            this.dataRecievePanel.Name = "dataRecievePanel";
            this.dataRecievePanel.Size = new System.Drawing.Size(770, 218);
            this.dataRecievePanel.TabIndex = 2;
            // 
            // dataRecieveLabel
            // 
            this.dataRecieveLabel.BackColor = System.Drawing.SystemColors.MenuBar;
            this.dataRecieveLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataRecieveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataRecieveLabel.Location = new System.Drawing.Point(0, 0);
            this.dataRecieveLabel.Name = "dataRecieveLabel";
            this.dataRecieveLabel.Size = new System.Drawing.Size(770, 218);
            this.dataRecieveLabel.TabIndex = 0;
            this.dataRecieveLabel.Text = "String Recieved";
            this.dataRecieveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.sendFileButton);
            this.tabPage2.Controls.Add(this.filesListView);
            this.tabPage2.Controls.Add(this.fileNamePanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(849, 457);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "File Transfer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // filesListView
            // 
            this.filesListView.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filesListView.HideSelection = false;
            this.filesListView.Location = new System.Drawing.Point(33, 90);
            this.filesListView.Name = "filesListView";
            this.filesListView.Size = new System.Drawing.Size(770, 218);
            this.filesListView.TabIndex = 1;
            this.filesListView.UseCompatibleStateImageBehavior = false;
            this.filesListView.SelectedIndexChanged += new System.EventHandler(this.filesListView_SelectedIndexChanged);
            // 
            // fileNamePanel
            // 
            this.fileNamePanel.Controls.Add(this.fileNameLabel);
            this.fileNamePanel.Location = new System.Drawing.Point(28, 20);
            this.fileNamePanel.Name = "fileNamePanel";
            this.fileNamePanel.Size = new System.Drawing.Size(782, 64);
            this.fileNamePanel.TabIndex = 0;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.fileNameLabel.Location = new System.Drawing.Point(0, 0);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(782, 64);
            this.fileNameLabel.TabIndex = 1;
            this.fileNameLabel.Text = "Choose file to send ";
            this.fileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.connectingLabel);
            this.panel1.Controls.Add(this.connectingPictureBox);
            this.panel1.Location = new System.Drawing.Point(12, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(826, 37);
            this.panel1.TabIndex = 2;
            // 
            // connectingLabel
            // 
            this.connectingLabel.AutoSize = true;
            this.connectingLabel.ForeColor = System.Drawing.Color.Navy;
            this.connectingLabel.Location = new System.Drawing.Point(392, 8);
            this.connectingLabel.Name = "connectingLabel";
            this.connectingLabel.Size = new System.Drawing.Size(102, 20);
            this.connectingLabel.TabIndex = 2;
            this.connectingLabel.Text = "Connecting...";
            this.connectingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // connectingPictureBox
            // 
            this.connectingPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.connectingPictureBox.Location = new System.Drawing.Point(371, 13);
            this.connectingPictureBox.Name = "connectingPictureBox";
            this.connectingPictureBox.Size = new System.Drawing.Size(15, 15);
            this.connectingPictureBox.TabIndex = 4;
            this.connectingPictureBox.TabStop = false;
            // 
            // configurationsLabel
            // 
            this.configurationsLabel.BackColor = System.Drawing.Color.Transparent;
            this.configurationsLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.configurationsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.configurationsLabel.Location = new System.Drawing.Point(3, 8);
            this.configurationsLabel.Name = "configurationsLabel";
            this.configurationsLabel.Size = new System.Drawing.Size(94, 29);
            this.configurationsLabel.TabIndex = 3;
            this.configurationsLabel.Text = "Settings";
            this.configurationsLabel.Click += new System.EventHandler(this.configurationButton_click);
            this.configurationsLabel.MouseLeave += new System.EventHandler(this.settingsPanel_MouseLeave);
            this.configurationsLabel.MouseHover += new System.EventHandler(this.settingsPanel_MouseHover);
            // 
            // settingsPanel
            // 
            this.settingsPanel.BackColor = System.Drawing.Color.Transparent;
            this.settingsPanel.Controls.Add(this.configurationsLabel);
            this.settingsPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.settingsPanel.Location = new System.Drawing.Point(1, 6);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(88, 37);
            this.settingsPanel.TabIndex = 2;
            this.settingsPanel.MouseLeave += new System.EventHandler(this.settingsPanel_MouseLeave);
            this.settingsPanel.MouseHover += new System.EventHandler(this.settingsPanel_MouseHover);
            // 
            // sendFileButton
            // 
            this.sendFileButton.ForeColor = System.Drawing.Color.Teal;
            this.sendFileButton.Location = new System.Drawing.Point(370, 365);
            this.sendFileButton.Name = "sendFileButton";
            this.sendFileButton.Size = new System.Drawing.Size(75, 27);
            this.sendFileButton.TabIndex = 2;
            this.sendFileButton.Text = "Send";
            this.sendFileButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumTurquoise;
            this.ClientSize = new System.Drawing.Size(841, 551);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MinimumSize = new System.Drawing.Size(515, 400);
            this.Name = "MainForm";
            this.Text = "Terminal Project";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.dataRecievePanel.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.fileNamePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.connectingPictureBox)).EndInit();
            this.settingsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button sendButton;

        // Text Box
        private System.Windows.Forms.TextBox dataToSendTextBox;

        // Labels
        private System.Windows.Forms.Label enterStringLabel;
        private System.Windows.Forms.Label connectingLabel;

        // Tabs
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label configurationsLabel;
        private System.Windows.Forms.PictureBox connectingPictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.Panel dataRecievePanel;
        private System.Windows.Forms.Label dataRecieveLabel;
        private System.Windows.Forms.Panel fileNamePanel;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Button nonFormatButon;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ListView filesListView;
        private System.Windows.Forms.Button sendFileButton;
    }
}

