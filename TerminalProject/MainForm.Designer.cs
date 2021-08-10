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
            this.stringRecieveRichTextBox = new TerminalProject.Source_files.DisabledRichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataRecieveRichTextBox = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.connectingLabel = new System.Windows.Forms.Label();
            this.connectingPictureBox = new System.Windows.Forms.PictureBox();
            this.configurationsLabel = new System.Windows.Forms.Label();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.sendButton.Location = new System.Drawing.Point(744, 45);
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
            this.dataToSendTextBox.Location = new System.Drawing.Point(35, 45);
            this.dataToSendTextBox.Name = "dataToSendTextBox";
            this.dataToSendTextBox.Size = new System.Drawing.Size(703, 30);
            this.dataToSendTextBox.TabIndex = 0;
            this.dataToSendTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataToSendTextBox_KeyDown);
            // 
            // enterStringLabel
            // 
            this.enterStringLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.enterStringLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.enterStringLabel.Location = new System.Drawing.Point(31, 17);
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
            this.tabControl1.ItemSize = new System.Drawing.Size(245, 35);
            this.tabControl1.Location = new System.Drawing.Point(-3, 54);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(191, 2);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(851, 500);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.stringRecieveRichTextBox);
            this.tabPage1.Controls.Add(this.sendButton);
            this.tabPage1.Controls.Add(this.enterStringLabel);
            this.tabPage1.Controls.Add(this.dataToSendTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(843, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chat";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // stringRecieveRichTextBox
            // 
            this.stringRecieveRichTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stringRecieveRichTextBox.Location = new System.Drawing.Point(35, 87);
            this.stringRecieveRichTextBox.Name = "stringRecieveRichTextBox";
            this.stringRecieveRichTextBox.Size = new System.Drawing.Size(770, 346);
            this.stringRecieveRichTextBox.TabIndex = 1;
            this.stringRecieveRichTextBox.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataRecieveRichTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(843, 457);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataRecieveRichTextBox
            // 
            this.dataRecieveRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataRecieveRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.dataRecieveRichTextBox.Name = "dataRecieveRichTextBox";
            this.dataRecieveRichTextBox.Size = new System.Drawing.Size(837, 451);
            this.dataRecieveRichTextBox.TabIndex = 0;
            this.dataRecieveRichTextBox.Text = "";
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumTurquoise;
            this.ClientSize = new System.Drawing.Size(848, 551);
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
            this.tabPage2.ResumeLayout(false);
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
        private System.Windows.Forms.RichTextBox dataRecieveRichTextBox;
        private Source_files.DisabledRichTextBox stringRecieveRichTextBox;

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
    }
}

